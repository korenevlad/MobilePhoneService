using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneService.DataAccess.Repository;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using MobilePhoneService.Models.Search;
using System.Diagnostics;

namespace MobilePhoneServiceWeb.Controllers
{
    public class ClientController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ClientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ClientSearch clientSearch = new ClientSearch
            {
                objClientForSearch = null,
                listOfClient = _unitOfWork.Client.GetAll().ToList()
            };
            return View(clientSearch);
        }



        [HttpPost]
        public IActionResult Index(ClientSearch searchObj)
        {
            IEnumerable<Client> query = _unitOfWork.Client.GetAll();

            if (!string.IsNullOrEmpty(searchObj.objClientForSearch.client_id_EoS))
            {
                int.TryParse(searchObj.objClientForSearch.client_id_EoS, out int clientId);
                query = query.Where(c => c.client_id == clientId);
            }

            if (!string.IsNullOrEmpty(searchObj.objClientForSearch.name_EoS))
            {
                query = query.Where(c => c.name.Contains(searchObj.objClientForSearch.name_EoS));
            }
            if (!string.IsNullOrEmpty(searchObj.objClientForSearch.surname_EoS))
            {
                query = query.Where(c => c.surname.Contains(searchObj.objClientForSearch.surname_EoS));
            }
            if (!string.IsNullOrEmpty(searchObj.objClientForSearch.phone_number_EoS))
            {
                query = query.Where(c => c.phone_number.Contains(searchObj.objClientForSearch.phone_number_EoS));
            }

            searchObj.listOfClient = query.ToList();

            return View(searchObj);
        }




        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0) //Добавление нового производителя
            {
                return View(new Client());
            }
            else //Обновление существующего производителя
            {
                Client findedClient = _unitOfWork.Client.Get(u => u.client_id == id);
                return View(findedClient);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Client obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.client_id == 0)
                {
                    _unitOfWork.Client.Add(obj);
                    TempData["success"] = "Клиент добавлен успешно!";
                }
                else
                {
                    _unitOfWork.Client.Update(obj);
                    TempData["success"] = "Клиент обновлен успешно!";
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
            var obj = _unitOfWork.Client.Get(u => u.client_id == id);
            if (obj == null)
            {
                return Json(new { succes = false, message = "Ошибка при удалении клиента!" });
            }
            try
            {
                _unitOfWork.Client.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Клиент удален успешно!" });
            }
            catch
            {
                return Json(new { success = false, message = "Удаление невозможно! <br> Есть заявки на ремонт, в которых указан данный клиент!" });
            }
        }

        #endregion
    }
}