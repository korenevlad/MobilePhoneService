using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models
{
    public class Phone_specification
    {
        public enum RamOptions
        {
            [Display(Name = "128 MB")]
            Ram128 = 128,

            [Display(Name = "256 MB")]
            Ram256 = 256
        }

        [Key]
        public int specification_id { get; set; }

        [Required]
        [DisplayName("RAM")]
        public int ram { get; set; }
    }
}
