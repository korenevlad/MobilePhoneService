using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.ViewModel;

namespace MobilePhoneServiceWeb.Controllers
{
    public class PhoneSpecificationController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public PhoneSpecificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Phone_specification> phineSpecificationList = _unitOfWork.PhoneSpecification.GetAll(includeProperties: "Cpu_of_specification,Operating_system_of_specification").ToList();
            return View(phineSpecificationList);
        }


        public IActionResult Upsert(int? specification_id)
        {
            IEnumerable<SelectListItem> listItemCpu = _unitOfWork.Cpu.GetAll().Select(u =>
                new SelectListItem { Text = u.model.ToString(), Value = u.cpu_id.ToString() });

            IEnumerable<SelectListItem> listItemOS = _unitOfWork.OperatingSystem.GetAll().Select(u =>
                new SelectListItem { Text = u.operating_system_name.ToString(), Value = u.operating_system_id.ToString() });

            PhoneSpecificationVM phoneSpecification_obj = new PhoneSpecificationVM
            {
                PhoneSpecification = new Phone_specification(),
                CpuList = listItemCpu,
                OpearatingSystemList = listItemOS
            };

            if (specification_id == null || specification_id == 0)
            {
                return View(phoneSpecification_obj);
            }
            else
            {
                phoneSpecification_obj.PhoneSpecification = _unitOfWork.PhoneSpecification.Get(u => u.specification_id == specification_id);
                return View(phoneSpecification_obj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PhoneSpecificationVM phoneSpecification_obj)
        {
            if (ModelState.IsValid)
            {
                if (phoneSpecification_obj.PhoneSpecification.specification_id == 0)
                {
                    _unitOfWork.PhoneSpecification.Add(phoneSpecification_obj.PhoneSpecification);
                    TempData["success"] = "Спецификация добавлена успешно!";
                }
                else
                {
                    _unitOfWork.PhoneSpecification.Update(phoneSpecification_obj.PhoneSpecification);
                    TempData["success"] = "Спецификация обновлена успешно!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                phoneSpecification_obj.CpuList = _unitOfWork.Cpu.GetAll().Select(u =>
                    new SelectListItem { Text = u.model.ToString(), Value = u.cpu_id.ToString() });

                phoneSpecification_obj.OpearatingSystemList = _unitOfWork.OperatingSystem.GetAll().Select(u =>
                    new SelectListItem { Text = u.operating_system_name.ToString(), Value = u.operating_system_id.ToString() });

                return View(phoneSpecification_obj);
            }
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Phone_specification> phoneSpecificationList = _unitOfWork.PhoneSpecification.GetAll(includeProperties: "Cpu_of_specification,Operating_system_of_specification").ToList();
            return Json(new { data = phoneSpecificationList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Phone_specification phoneSpecificationToBeDelete = _unitOfWork.PhoneSpecification.Get(u => u.specification_id == id);
            if (phoneSpecificationToBeDelete == null)
            {
                return Json(new { success = false, message = "Ошибка при удалении cпецификации!" });
            }
            try
            {
                _unitOfWork.PhoneSpecification.Remove(phoneSpecificationToBeDelete);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Спецификация удалена успешно!" });
            }
            catch
            {
                return RedirectToAction("Index", new { infoError = "Удаление невозможно! Есть модели телефонов, у которых есть данная спецификация!" });
            }
        }

        #endregion
    }
}
