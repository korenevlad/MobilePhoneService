using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DataAccess.Repository;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using System.Diagnostics;

namespace MobilePhoneServiceWeb.Controllers
{
    public class ManufacturerController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ManufacturerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Manufacturer> manufacturer = _unitOfWork.Manufacturer.GetAll().ToList();
            return View(manufacturer);
        }

        public IActionResult Upsert(int? manufacturer_id)
        {
            if (manufacturer_id == null || manufacturer_id == 0) //Добавление нового производителя
            {
                return View(new Manufacturer());
            }
            else //Обновление существующего производителя
            {
                Manufacturer findedManufacturer = _unitOfWork.Manufacturer.Get(u => u.manufacturer_id == manufacturer_id);
                return View(findedManufacturer);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Manufacturer obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.manufacturer_id == 0)
                {
                    _unitOfWork.Manufacturer.Add(obj);
                    TempData["success"] = "Производитель добавлен успешно!";
                }
                else
                {
                    _unitOfWork.Manufacturer.Update(obj);
                    TempData["success"] = "Производитель обновлен успешно!";
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
            List<Manufacturer> manufacturerList = _unitOfWork.Manufacturer.GetAll().ToList();
            return Json(new { data = manufacturerList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Manufacturer.Get(u => u.manufacturer_id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Ошибка при удалении производителя!" });
            }
            try
            {
                _unitOfWork.Manufacturer.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Производитель удален успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Удаление невозможно! <br> Есть модели телефонов, для которых указан данный производитель!" });
            }
        }


        #endregion
    }
}