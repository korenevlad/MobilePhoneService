using MobilePhoneService.Models.Search.ElemetsOfSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search
{
    public class HistoryOfRepairSearch
    {
        public HistoryOfRepairElementSearch objHistoryOfRepairForSearch { get; set; }
        public List<History_of_repair> listOfHistory_of_repair { get; set; }
    }
}
