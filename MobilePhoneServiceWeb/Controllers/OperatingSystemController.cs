using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DataAccess.Repository;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using System.Diagnostics;

namespace MobilePhoneServiceWeb.Controllers
{
    public class OperatingSystemController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public OperatingSystemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Operating_system> operatingSystems = _unitOfWork.OperatingSystem.GetAll().ToList();
            return View(operatingSystems);
        }

        public IActionResult Upsert(int? operating_system_id)
        {
            if (operating_system_id == null || operating_system_id == 0) //Добавление нового производителя
            {
                return View(new Operating_system());
            }
            else //Обновление существующего производителя
            {
                Operating_system findedOperatingSystem = _unitOfWork.OperatingSystem.Get(u => u.operating_system_id == operating_system_id);
                return View(findedOperatingSystem);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Operating_system obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.operating_system_id == 0)
                {
                    _unitOfWork.OperatingSystem.Add(obj);
                    TempData["success"] = "Операционная система добавлена успешно!";
                }
                else
                {
                    _unitOfWork.OperatingSystem.Update(obj);
                    TempData["success"] = "Операционная система обновлена успешно!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else { return View(obj); }
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Operating_system> operatingSystemList = _unitOfWork.OperatingSystem.GetAll().ToList();
            return Json(new { data = operatingSystemList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.OperatingSystem.Get(u => u.operating_system_id == id);
            if (obj == null)
            {
                return Json(new { succes = false, message = "Ошибка при удалении операционной системы!" });
            }
            try
            {
                _unitOfWork.OperatingSystem.Remove(obj);
                _unitOfWork.Save();
                return Json(new { succes = true, message = "Операционная система удалена успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Удаление невозможно! Есть спецификации телефонов, у которых есть данная операционная система!" });
            }
        }

        #endregion
    }
}