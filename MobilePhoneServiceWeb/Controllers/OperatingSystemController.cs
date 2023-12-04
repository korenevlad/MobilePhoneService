using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DataAccess.Repository;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
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
            OperatingSystemSearch operatingSystemSearch = new OperatingSystemSearch
            {
                objOperatingSystemForSearch = null,
                listOfOperatingSystem = _unitOfWork.OperatingSystem.GetAll().ToList()
            };
            return View(operatingSystemSearch);
        }

        [HttpPost]
        public IActionResult Index(OperatingSystemSearch searchObj)
        {
            IEnumerable<Operating_system> query = _unitOfWork.OperatingSystem.GetAll();
            if (!string.IsNullOrEmpty(searchObj.objOperatingSystemForSearch.operating_system_id_EoS))
            {
                query = query.Where(c => c.operating_system_id.ToString().Contains(searchObj.objOperatingSystemForSearch.operating_system_id_EoS));
            }
            if (!string.IsNullOrEmpty(searchObj.objOperatingSystemForSearch.operating_system_name_EoS))
            {
                query = query.Where(c => c.operating_system_name.Contains(searchObj.objOperatingSystemForSearch.operating_system_name_EoS));
            }
            if (!string.IsNullOrEmpty(searchObj.objOperatingSystemForSearch.operating_system_version_EoS))
            {
                query = query.Where(c => c.operating_system_version.ToString().Contains(searchObj.objOperatingSystemForSearch.operating_system_version_EoS));
            }
            searchObj.listOfOperatingSystem = query.ToList();

            return View(searchObj);
        }


        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0) //Добавление нового производителя
            {
                return View(new Operating_system());
            }
            else //Обновление существующего производителя
            {
                Operating_system findedOperatingSystem = _unitOfWork.OperatingSystem.Get(u => u.operating_system_id == id);
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
                return Json(new { success = true, message = "Операционная система удалена успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Удаление невозможно! <br> Есть спецификации телефонов, у которых есть данная операционная система!" });
            }
        }

        #endregion
    }
}