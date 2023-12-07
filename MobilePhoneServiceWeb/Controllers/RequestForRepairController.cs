using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
using MobilePhoneService.Models.ViewModel;

namespace MobilePhoneServiceWeb.Controllers
{
    public class RequestForRepairController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public RequestForRepairController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Request_for_repair> listOfRfr = _unitOfWork.RequestForRepair.GetAll(
                includeProperties: "Phone_Model_of_request_for_repair,Service_of_request_for_repair,Client_of_request_for_repair").ToList();
            return View(listOfRfr);
        }


        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> listItemPhoneModel = _unitOfWork.PhoneModel.GetAll().Select(u =>
                new SelectListItem { Text = u.name.ToString(), Value = u.model_id.ToString() });

            IEnumerable<SelectListItem> listItemService = _unitOfWork.Service.GetAll().Select(u =>
                new SelectListItem { Text = u.description.ToString(), Value = u.service_id.ToString() });

            IEnumerable<SelectListItem> listItemClient = _unitOfWork.Client.GetAll().Select(u =>
                new SelectListItem { Text = u.name.ToString() + " " + u.surname.ToString(), Value = u.client_id.ToString() });

            RequestForRepairVM requestForRepair_obj = new RequestForRepairVM
            {
                request_for_repair = new Request_for_repair(),
                phoneModelList = listItemPhoneModel,
                servicetList = listItemService,
                clientList = listItemClient
            };

            if (id == null || id == 0)
            {
                return View(requestForRepair_obj);
            }
            else
            {
                requestForRepair_obj.request_for_repair = _unitOfWork.RequestForRepair.Get(u => u.request_id == id);
                return View(requestForRepair_obj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(RequestForRepairVM requestForRepair_obj)
        {
            if (ModelState.IsValid)
            {
                if (requestForRepair_obj.request_for_repair.request_id == 0)
                {
                    _unitOfWork.RequestForRepair.Add(requestForRepair_obj.request_for_repair);
                    TempData["success"] = "Заявка на ремонт добавлена успешно!";
                }
                else
                {
                    _unitOfWork.RequestForRepair.Update(requestForRepair_obj.request_for_repair);
                    TempData["success"] = "Заявка на ремонт обновлена успешно!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                requestForRepair_obj.phoneModelList = _unitOfWork.PhoneModel.GetAll().Select(u => 
                    new SelectListItem { Text = u.name.ToString(), Value = u.model_id.ToString() });

                requestForRepair_obj.servicetList = _unitOfWork.Service.GetAll().Select(u =>
                    new SelectListItem { Text = u.description.ToString(), Value = u.service_id.ToString() });

                requestForRepair_obj.clientList = _unitOfWork.Client.GetAll().Select(u =>
                new SelectListItem { Text = u.surname.ToString(), Value = u.client_id.ToString() });

                return View(requestForRepair_obj);
            }
        }

        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            History_of_repair historyOfRepairToBeDelete = _unitOfWork.HistoryOfRepair.Get(u => u.request_id == id);
            Request_for_repair requestForRepairToBeDelete = _unitOfWork.RequestForRepair.Get(u => u.request_id == id);
           
            if (historyOfRepairToBeDelete == null || requestForRepairToBeDelete == null)
            {
                return Json(new { success = false, message = "Ошибка при удалении заявки на ремонт и её истории!" });
            }
            try
            {
                _unitOfWork.HistoryOfRepair.Remove(historyOfRepairToBeDelete);
                _unitOfWork.RequestForRepair.Remove(requestForRepairToBeDelete);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Заявка на ремонт и её история удалены успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Непредвиденная ошибка при удалении!" });
            }
        }

        #endregion
    }
}
