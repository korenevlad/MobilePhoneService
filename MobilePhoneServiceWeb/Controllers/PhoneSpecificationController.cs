using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
using MobilePhoneService.Models.ViewModel;
using System.Collections.Generic;

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
            PhoneSpecificationSearch phoneSpecificationSearch = new PhoneSpecificationSearch
            {
                obj_PhoneSpecification_ForSearch = null,
                listOf_PhoneSpecification = _unitOfWork.PhoneSpecification.GetAll(includeProperties: "Cpu_of_specification,Operating_system_of_specification").ToList()
            };
            return View(phoneSpecificationSearch);
        }

        [HttpPost]
        public IActionResult Index(PhoneSpecificationSearch searchObj)
        {
            IEnumerable<Phone_specification> query = _unitOfWork.PhoneSpecification.GetAll(includeProperties: "Cpu_of_specification,Operating_system_of_specification");

            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.specification_id_EoS))
            {
                int.TryParse(searchObj.obj_PhoneSpecification_ForSearch.specification_id_EoS, out int specId);
                query = query.Where(c => c.specification_id == specId);
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.ram_EoS))
            {
                int.TryParse(searchObj.obj_PhoneSpecification_ForSearch.ram_EoS, out int specRam);
                query = query.Where(c => c.ram == specRam);
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.internal_memory_EoS))
            {
                int.TryParse(searchObj.obj_PhoneSpecification_ForSearch.internal_memory_EoS, out int specInternal_memory);
                query = query.Where(c => c.internal_memory == specInternal_memory);
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.screen_size_EoS))
            {
                int.TryParse(searchObj.obj_PhoneSpecification_ForSearch.screen_size_EoS, out int specScreen_size);
                query = query.Where(c => c.screen_size == specScreen_size);
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.cpu_name_EoS))
            {
                query = query.Where(c => c.Cpu_of_specification.model.Contains(searchObj.obj_PhoneSpecification_ForSearch.cpu_name_EoS));
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.cpu_amount_cernels_EoS))
            {
                int.TryParse(searchObj.obj_PhoneSpecification_ForSearch.cpu_amount_cernels_EoS, out int specCpu_amount_cernels);
                query = query.Where(c => c.Cpu_of_specification.amount_cernels == specCpu_amount_cernels);
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.cpu_freq_EoS))
            {
                int.TryParse(searchObj.obj_PhoneSpecification_ForSearch.cpu_freq_EoS, out int specCpu_freq);
                query = query.Where(c => c.Cpu_of_specification.frequency == specCpu_freq);
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.operating_system_name_EoS))
            {
                query = query.Where(c => c.Operating_system_of_specification.operating_system_name.Contains(searchObj.obj_PhoneSpecification_ForSearch.operating_system_name_EoS));
            }
            if (!string.IsNullOrEmpty(searchObj.obj_PhoneSpecification_ForSearch.operating_system_version_EoS))
            {
                int.TryParse(searchObj.obj_PhoneSpecification_ForSearch.operating_system_version_EoS, out int specOS_version);
                query = query.Where(c => c.Operating_system_of_specification.operating_system_version == specOS_version);
            }
            searchObj.listOf_PhoneSpecification = query.ToList();

            return View(searchObj);
        }



        public IActionResult Upsert(int? id)
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

            if (id == null || id == 0)
            {
                return View(phoneSpecification_obj);
            }
            else
            {
                phoneSpecification_obj.PhoneSpecification = _unitOfWork.PhoneSpecification.Get(u => u.specification_id == id);
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
                return Json(new { success = false, message = "Удаление невозможно! <br> Есть модели телефонов, у которых есть данный раздел спецификаций!" });
            }
        }

        #endregion
    }
}
