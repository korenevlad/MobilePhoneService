using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
using MobilePhoneService.Models.ViewModel;

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


                if (history_of_repair_obj_second.history_id == 0)
                {
                    _unitOfWork.HistoryOfRepair.Add(history_of_repair_obj_second);
                    TempData["success"] = "История заявки добавлена успешно!";
                }
                else
                {
                    _unitOfWork.HistoryOfRepair.Update(history_of_repair_obj_second);
                    TempData["success"] = "История заявки обновлена успешно!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else { return View(history_of_repair_obj); }
        }


        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            History_of_repair historyOfRepairToBeDelete = _unitOfWork.HistoryOfRepair.Get(u => u.history_id == id);
            if (historyOfRepairToBeDelete == null)
            {
                return Json(new { success = false, message = "Ошибка при удалении истории заявки на ремонт!" });
            }
            _unitOfWork.HistoryOfRepair.Remove(historyOfRepairToBeDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "История заявки на ремонт удалена успешно!" });
        }

        #endregion
    }
}

