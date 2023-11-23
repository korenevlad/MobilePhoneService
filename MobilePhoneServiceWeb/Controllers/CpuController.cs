using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;

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
            List<Cpu> cpu = _unitOfWork.Cpu.GetAll().ToList();
            return View(cpu);
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

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Cpu> cpuList = _unitOfWork.Cpu.GetAll().ToList();
            return Json(new { data = cpuList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Cpu.Get(u => u.cpu_id == id);
            if (obj == null)
            {
                return Json(new { succes = false, message = "Ошибка при удалении процессора!" });
            }
            _unitOfWork.Cpu.Remove(obj);
            _unitOfWork.Save();
            return Json(new { succes = true, message = "Процессор удален успешно!" });
        }


        #endregion
    }
}
