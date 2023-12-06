using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.ViewModel
{
    public class RequestForRepairVM
    {
        public Request_for_repair request_for_repair { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> phoneModelList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> servicetList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> clientList { get; set; }
    }
}
