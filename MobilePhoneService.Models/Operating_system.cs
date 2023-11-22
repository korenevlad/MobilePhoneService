using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models
{
    public class Operating_system
    {
        [Key]
        public int operating_system_id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Name")]
        public string operating_system_name { get; set; }

        [Range(0, 17)]
        [DisplayName("Version")]
        [ValidateNever]
        public int? operating_system_version { get; set; }
    }
}
