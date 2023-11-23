using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.ViewModel
{
    public class PhoneSpecificationVM
    {
        public Phone_specification PhoneSpecification { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CpuList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> OpearatingSystemList { get; set; }
    }
}
