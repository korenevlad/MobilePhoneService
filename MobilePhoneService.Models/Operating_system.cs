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
        [MaxLength(50, ErrorMessage = "Имя не может превышать 50 символов!")]
        [DisplayName("Name")]
        public string operating_system_name { get; set; }

        [Required]
        [Range(1, 17, ErrorMessage = "Версия может быть в диапозоне от 1 до 17!")]
        [DisplayName("Version")]
        public int operating_system_version { get; set; }
    }
}
