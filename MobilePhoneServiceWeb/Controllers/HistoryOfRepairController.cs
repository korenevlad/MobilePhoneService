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
            List<History_of_repair> listOfHor = _unitOfWork.HistoryOfRepair.GetAll(
                includeProperties: "Request_for_repair_of_history_of_repair").ToList();
            return View(listOfHor);
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

