using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.ViewModel;

namespace MobilePhoneServiceWeb.Controllers
{
    public class PhoneModelController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public PhoneModelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Phone_model> phoneModelList = _unitOfWork.PhoneModel.GetAll(includeProperties: "Manufacturer_of_phone_model,Phone_Specification_of_phone_model").ToList();
            return View(phoneModelList);
        }


        public IActionResult Upsert(int? model_id)
        {
            IEnumerable<SelectListItem> listItemManufacturer = _unitOfWork.Manufacturer.GetAll().Select(u =>
                new SelectListItem { Text = u.manufacturer_name.ToString(), Value = u.manufacturer_id.ToString() });

            IEnumerable<SelectListItem> listItemSpecification = _unitOfWork.PhoneSpecification.GetAll().Select(u =>
                new SelectListItem { Text = u.specification_id.ToString(), Value = u.specification_id.ToString() });

            PhoneModelVM phoneModel_obj = new PhoneModelVM
            {
                phoneModel = new Phone_model(),
                ManufacturerList = listItemManufacturer,
                SpecificationList = listItemSpecification
            };

            if (model_id == null || model_id == 0)
            {
                return View(phoneModel_obj);
            }
            else
            {
                phoneModel_obj.phoneModel = _unitOfWork.PhoneModel.Get(u => u.model_id == model_id);
                return View(phoneModel_obj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PhoneModelVM phoneModel_obj)
        {
            if (ModelState.IsValid)
            {
                if (phoneModel_obj.phoneModel.specification_id == 0)
                {
                    _unitOfWork.PhoneModel.Add(phoneModel_obj.phoneModel);
                    TempData["success"] = "Модель телефона добавлена успешно!";
                }
                else
                {
                    _unitOfWork.PhoneModel.Update(phoneModel_obj.phoneModel);
                    TempData["success"] = "Модель телефона обновлена успешно!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                phoneModel_obj.ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(u =>
                    new SelectListItem { Text = u.manufacturer_name.ToString(), Value = u.manufacturer_id.ToString() });

                phoneModel_obj.SpecificationList = _unitOfWork.PhoneSpecification.GetAll().Select(u =>
                    new SelectListItem { Text = u.specification_id.ToString(), Value = u.specification_id.ToString() });

                return View(phoneModel_obj);
            }
        }

        public IActionResult SpecificationView(int? specification_id)
        {
            Phone_specification phone_specificationObj = _unitOfWork.PhoneSpecification.Get(u => u.specification_id == specification_id, 
                includeProperties: "Cpu_of_specification,Operating_system_of_specification");
            return View(phone_specificationObj);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Phone_model> phoneModelList = _unitOfWork.PhoneModel.GetAll(includeProperties: "Manufacturer_of_phone_model,Phone_Specification_of_phone_model").ToList();
            return Json(new { data = phoneModelList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Phone_model phoneModelToBeDelete = _unitOfWork.PhoneModel.Get(u => u.model_id == id);
            if (phoneModelToBeDelete == null)
            {
                return Json(new { success = false, message = "Ошибка при удалении модели телефона!" });
            }
            try
            {
                _unitOfWork.PhoneModel.Remove(phoneModelToBeDelete);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Модель телефона удалена успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Удаление невозможно! <br> Есть заявки на ремонт, в которых указан данный мобильный телефон!" });
            }
        }

        #endregion
    }
}
