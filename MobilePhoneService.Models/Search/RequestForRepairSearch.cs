using MobilePhoneService.Models.Search.ElemetsOfSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search
{
    public class RequestForRepairSearch
    {
        public RequestForRepairElementSearch objRequestForRepairForSearch { get; set; }
        public List<Request_for_repair> listOfRequestForRepair { get; set; }
    }
}
