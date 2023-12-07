using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
using MobilePhoneService.Models.ViewModel;
using MySqlConnector;

namespace MobilePhoneServiceWeb.Controllers
{
    public class HistoryOfRepairController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public HistoryOfRepairController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            HistoryOfRepairSearch historyOfRepairSearch = new HistoryOfRepairSearch
            {
                objHistoryOfRepairForSearch = null,
                listOfHistory_of_repair = _unitOfWork.HistoryOfRepair.GetAll(
                    includeProperties: "Request_for_repair_of_history_of_repair").ToList()
            };
            return View(historyOfRepairSearch);
        }



        [HttpPost]
        public IActionResult Index(HistoryOfRepairSearch historyOfRepairSearch)
        {
            IEnumerable<History_of_repair> query = _unitOfWork.HistoryOfRepair.GetAll(
                    includeProperties: "Request_for_repair_of_history_of_repair");

            if (!string.IsNullOrEmpty(historyOfRepairSearch.objHistoryOfRepairForSearch.request_id_EoS))
            {
                int.TryParse(historyOfRepairSearch.objHistoryOfRepairForSearch.request_id_EoS, out int rfr_Id);
                query = query.Where(c => c.request_id == rfr_Id);
            }
            if (!string.IsNullOrEmpty(historyOfRepairSearch.objHistoryOfRepairForSearch.datetime_of_request_EoS))
            {
                query = query.Where(c => c.Request_for_repair_of_history_of_repair.datetime_of_request.ToString().Contains(historyOfRepairSearch.objHistoryOfRepairForSearch.datetime_of_request_EoS));
            }
            if (!string.IsNullOrEmpty(historyOfRepairSearch.objHistoryOfRepairForSearch.history_id_EoS))
            {
                int.TryParse(historyOfRepairSearch.objHistoryOfRepairForSearch.history_id_EoS, out int hor_Id);
                query = query.Where(c => c.history_id == hor_Id);
            }
            if (!string.IsNullOrEmpty(historyOfRepairSearch.objHistoryOfRepairForSearch.date_start_EoS))
            {
                query = query.Where(c => c.date_start.ToString().Contains(historyOfRepairSearch.objHistoryOfRepairForSearch.date_start_EoS));
            }
            if (!string.IsNullOrEmpty(historyOfRepairSearch.objHistoryOfRepairForSearch.date_end_EoS))
            {
                query = query.Where(c => c.date_end.ToString().Contains(historyOfRepairSearch.objHistoryOfRepairForSearch.date_end_EoS));
            }


            historyOfRepairSearch.listOfHistory_of_repair = query.ToList();

            return View(historyOfRepairSearch);
        }







        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new History_of_repair());
            }
            else
            {
                History_of_repair history_of_repair_obj = _unitOfWork.HistoryOfRepair.Get(u => u.history_id == id, 
                    includeProperties: "Request_for_repair_of_history_of_repair");
                return View(history_of_repair_obj);
            }
        }


        [HttpPost]
        public IActionResult Upsert(History_of_repair history_of_repair_obj)
        {
            if (ModelState.IsValid)
            {
                History_of_repair history_of_repair_obj_second = _unitOfWork.HistoryOfRepair.Get(u => u.history_id == history_of_repair_obj.history_id,
                    includeProperties: "Request_for_repair_of_history_of_repair");
                history_of_repair_obj_second.date_start = history_of_repair_obj.date_start;
                history_of_repair_obj_second.date_end = history_of_repair_obj.date_end;

                try
                {
                    if (history_of_repair_obj_second.history_id == 0)
                    {
                        _unitOfWork.HistoryOfRepair.Add(history_of_repair_obj_second);
                    }
                    else
                    {
                        _unitOfWork.HistoryOfRepair.Update(history_of_repair_obj_second);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "История заявки обновлена успешно!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException is MySqlException mysqlException_1 && 
                        mysqlException_1.Message.Contains("Нельзя вставить значение в date_end, так как в date_start стоит NULL"))
                    {
                        TempData["error"] = "Нельзя вставить значение в 'Дата конца работ', так как в 'Дата начала работ' стоит NULL";
                    }
                    if (ex.InnerException is MySqlException mysqlException_2 &&
                        mysqlException_2.Message.Contains("date_end не может быть меньше date_start"))
                    {
                        TempData["error"] = "'Дата конца работ' не может быть меньше 'Дата начала работ'";
                    }
                    if (ex.InnerException is MySqlException mysqlException_3 &&
                        mysqlException_3.Message.Contains("date_start не может быть меньше datetime_of_request"))
                    {
                        TempData["error"] = "'Дата начала работ' не может быть меньше 'Дата создания заявки'";
                    }
                    return View(history_of_repair_obj);
                }
            }
            else { return View(history_of_repair_obj); }
        }


        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            History_of_repair historyOfRepairToBeDelete = _unitOfWork.HistoryOfRepair.Get(u => u.history_id == id);
            Request_for_repair requestForRepairToBeDelete = _unitOfWork.RequestForRepair.Get(u => 
                u.request_id == historyOfRepairToBeDelete.request_id);

            if (historyOfRepairToBeDelete == null || requestForRepairToBeDelete == null)
            {
                return Json(new { success = false, message = "Ошибка при удалении истории заявки на ремонт и самой заявки!" });
            }
            try
            {
                _unitOfWork.HistoryOfRepair.Remove(historyOfRepairToBeDelete);
                _unitOfWork.RequestForRepair.Remove(requestForRepairToBeDelete);
                _unitOfWork.Save();
                return Json(new { success = true, message = "История заявки на ремонт и сама заявка удалены успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Непредвиденная ошибка при удалении!" });
            }
        }

        #endregion
    }
}

