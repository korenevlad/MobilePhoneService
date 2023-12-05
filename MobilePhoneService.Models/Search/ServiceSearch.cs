using MobilePhoneService.Models.Search.ElemetsOfSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search
{
    public class ServiceSearch
    {
        public ServiceElementSearch objServiceForSearch { get; set; }
        public List<Service> listOfService { get; set; }
    }
}
