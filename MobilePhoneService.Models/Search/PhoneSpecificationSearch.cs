using MobilePhoneService.Models.Search.ElemetsOfSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search
{
    public class PhoneSpecificationSearch
    {
        public PhoneSpecificationElementSearch obj_PhoneSpecification_ForSearch { get; set; }
        public List<Phone_specification> listOf_PhoneSpecification { get; set; }
    }
}
