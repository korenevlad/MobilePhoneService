using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.ViewModel
{
    public class PhoneModelVM
    {
        public Phone_model phoneModel { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SpecificationList { get; set; }
    }
}
