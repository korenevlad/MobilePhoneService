using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
using MySqlConnector;
using System;

namespace MobilePhoneServiceWeb.Controllers
{
    public class CpuController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public CpuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            CpuSearch cpuSearch = new CpuSearch
            {
                objCpuForSearch = null,
                listOfCpu = _unitOfWork.Cpu.GetAll().ToList()
            };
            return View(cpuSearch);
        }

        [HttpPost]
        public IActionResult Index(CpuSearch searchObj)
        {
            IEnumerable<Cpu> query = _unitOfWork.Cpu.GetAll();

            if (!string.IsNullOrEmpty(searchObj.objCpuForSearch.cpu_id_EoS))
            {
                int.TryParse(searchObj.objCpuForSearch.cpu_id_EoS, out int cpuId);
                query = query.Where(c => c.cpu_id == cpuId);
            }
            if (!string.IsNullOrEmpty(searchObj.objCpuForSearch.model_EoS))
            {
                query = query.Where(c => c.model.Contains(searchObj.objCpuForSearch.model_EoS));
            }
            if (!string.IsNullOrEmpty(searchObj.objCpuForSearch.frequency_EoS))
            {
                int.TryParse(searchObj.objCpuForSearch.frequency_EoS, out int cpuFrequency);
                query = query.Where(c => c.frequency == cpuFrequency);
            }
            if (!string.IsNullOrEmpty(searchObj.objCpuForSearch.amount_cernels_EoS))
            {
                int.TryParse(searchObj.objCpuForSearch.amount_cernels_EoS, out int cpuAmount_cernels);
                query = query.Where(c => c.amount_cernels == cpuAmount_cernels);
            }

            searchObj.listOfCpu = query.ToList();

            return View(searchObj);
        }

        public IActionResult Upsert(int? cpu_id)
        {
            if (cpu_id == null || cpu_id == 0) //Добавление нового производителя
            {
                return View(new Cpu());
            }
            else //Обновление существующего производителя
            {
                Cpu findedCpu = _unitOfWork.Cpu.Get(u => u.cpu_id == cpu_id);
                return View(findedCpu);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Cpu obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.cpu_id == 0)
                {
                    _unitOfWork.Cpu.Add(obj);
                    TempData["success"] = "Процессор добавлен успешно!";
                }
                else
                {
                    _unitOfWork.Cpu.Update(obj);
                    TempData["success"] = "Процессор обновлен успешно!";
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
            var obj = _unitOfWork.Cpu.Get(u => u.cpu_id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Ошибка при удалении процессора!" });
            }
            try
            {
                _unitOfWork.Cpu.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Процессор удалён успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Удаление невозможно! <br> Есть спецификации телефонов, у которых есть данный процессор!" });
            }
        }

        #endregion
    }
}
