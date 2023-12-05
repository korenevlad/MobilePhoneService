using MobilePhoneService.Models.Search.ElemetsOfSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search
{
    public class ManufacturerSearch
    {
        public ManufacturerElementSearch obj_Manufacturer_ForSearch { get; set; }
        public List<Manufacturer> listOf_Manufacturer { get; set; }
    }
}
