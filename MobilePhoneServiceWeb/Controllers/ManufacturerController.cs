using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DataAccess.Repository;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
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
            ManufacturerSearch manufacturerSearch = new ManufacturerSearch
            {
                obj_Manufacturer_ForSearch = null,
                listOf_Manufacturer = _unitOfWork.Manufacturer.GetAll().ToList()
            };
            return View(manufacturerSearch);
        }

        [HttpPost]
        public IActionResult Index(ManufacturerSearch searchObj)
        {
            IEnumerable<Manufacturer> query = _unitOfWork.Manufacturer.GetAll();

            if (!string.IsNullOrEmpty(searchObj.obj_Manufacturer_ForSearch.manufacturer_id_EoS))
            {
                int.TryParse(searchObj.obj_Manufacturer_ForSearch.manufacturer_id_EoS, out int manufacturerId);
                query = query.Where(c => c.manufacturer_id == manufacturerId);
            }

            if (!string.IsNullOrEmpty(searchObj.obj_Manufacturer_ForSearch.manufacturer_name_EoS))
            {
                query = query.Where(c => c.manufacturer_name.Contains(searchObj.obj_Manufacturer_ForSearch.manufacturer_name_EoS));
            }

            if (!string.IsNullOrEmpty(searchObj.obj_Manufacturer_ForSearch.country_EoS))
            {
                query = query.Where(c => c.country.Contains(searchObj.obj_Manufacturer_ForSearch.country_EoS));
            }

            searchObj.listOf_Manufacturer = query.ToList();

            return View(searchObj);
        }


        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0) //Добавление нового производителя
            {
                return View(new Manufacturer());
            }
            else //Обновление существующего производителя
            {
                Manufacturer findedManufacturer = _unitOfWork.Manufacturer.Get(u => u.manufacturer_id == id);
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


        [HttpDelete]
        public IActionResult Delete(int id)
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