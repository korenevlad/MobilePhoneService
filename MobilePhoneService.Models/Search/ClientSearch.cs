using MobilePhoneService.Models.Search.ElemetsOfSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search
{
    public class ClientSearch
    {
        public ClientElementSearch objClientForSearch { get; set; }
        public List<Client> listOfClient { get; set; }
    }
}
