using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DataAccess.Repository;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
using System.Diagnostics;

namespace MobilePhoneServiceWeb.Controllers
{
    public class ServiceController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ServiceSearch operatingSystemSearch = new ServiceSearch
            {
                objServiceForSearch = null,
                listOfService = _unitOfWork.Service.GetAll().ToList()
            };
            return View(operatingSystemSearch);
        }

        [HttpPost]
        public IActionResult Index(ServiceSearch searchObj)
        {
            IEnumerable<Service> query = _unitOfWork.Service.GetAll();

            if (!string.IsNullOrEmpty(searchObj.objServiceForSearch.service_id_EoS))
            {
                int.TryParse(searchObj.objServiceForSearch.service_id_EoS, out int serviceId);
                query = query.Where(c => c.service_id == serviceId);
            }

            if (!string.IsNullOrEmpty(searchObj.objServiceForSearch.name_EoS))
            {
                query = query.Where(c => c.description.Contains(searchObj.objServiceForSearch.name_EoS));
            }

            if (!string.IsNullOrEmpty(searchObj.objServiceForSearch.price_EoS))
            {
                int.TryParse(searchObj.objServiceForSearch.price_EoS, out int servicePrice);
                query = query.Where(c => c.price == servicePrice);
            }

            searchObj.listOfService = query.ToList();

            return View(searchObj);
        }


        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0) //Добавление нового производителя
            {
                return View(new Service());
            }
            else //Обновление существующего производителя
            {
                Service findedService = _unitOfWork.Service.Get(u => u.service_id == id);
                return View(findedService);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Service obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.service_id == 0)
                {
                    _unitOfWork.Service.Add(obj);
                    TempData["success"] = "Услуга добавлена успешно!";
                }
                else
                {
                    _unitOfWork.Service.Update(obj);
                    TempData["success"] = "Услуга обновлена успешно!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else { return View(obj); }
        }


        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Service.Get(u => u.service_id == id);
            if (obj == null)
            {
                return Json(new { succes = false, message = "Ошибка при удалении услуги!" });
            }
            try
            {
                _unitOfWork.Service.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Услуга удалена успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Удаление невозможно! <br> Есть заявки на ремонт, в которых указана данная услуга!" });
            }
        }

        #endregion
    }
}