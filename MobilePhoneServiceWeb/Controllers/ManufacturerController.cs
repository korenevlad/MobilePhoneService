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
            List<Manufacturer> manufacturers = _unitOfWork.Manufacturer.GetAll().ToList();
            return View(manufacturers);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}