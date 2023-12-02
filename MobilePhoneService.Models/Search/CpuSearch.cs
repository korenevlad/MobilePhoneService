using MobilePhoneService.Models.Search.ElemetsOfSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search
{
    public class CpuSearch
    {
        public CpuElementSearch objCpuForSearch { get; set; }
        public List<Cpu> listOfCpu { get; set; }
    }
}
