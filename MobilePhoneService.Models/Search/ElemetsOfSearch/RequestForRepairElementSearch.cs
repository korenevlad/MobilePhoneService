using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search.ElemetsOfSearch
{
    public class RequestForRepairElementSearch
    {
        public string? request_id_EoS { get; set; }
        public string? datetime_of_request_EoS { get; set; }
        public string? status_EoS { get; set; }
        public string? phone_model_name_EoS { get; set; }
        public string? service_EoS { get; set; }
        public string? client_name_EoS { get; set; }
        public string? client_surname_EoS { get; set; }
    }
}
