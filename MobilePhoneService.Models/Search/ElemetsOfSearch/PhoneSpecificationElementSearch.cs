using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models.Search.ElemetsOfSearch
{
    public class PhoneSpecificationElementSearch
    {
        public string? specification_id_EoS { get; set; }
        public string? ram_EoS { get; set; }
        public string? internal_memory_EoS { get; set; }
        public string? screen_size_EoS { get; set; }
        public string? cpu_name_EoS { get; set; }
        public string? cpu_amount_cernels_EoS { get; set; }
        public string? cpu_freq_EoS { get; set; }
        public string? operating_system_name_EoS { get; set; }
        public string? operating_system_version_EoS { get; set; }
    }
}
