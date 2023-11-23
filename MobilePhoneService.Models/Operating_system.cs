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

        [Required(ErrorMessage = "Поле Название обязательно для заполнения!")]
        [DisplayName("Name")]
        [MaxLength(50, ErrorMessage = "Имя не может превышать 50 символов!")]
        public string operating_system_name { get; set; }

        [Required(ErrorMessage = "Поле Версия обязательно для заполнения!")]
        [DisplayName("Version")]
        [Range(1, 17, ErrorMessage = "Версия может быть в диапозоне от 1 до 17!")]
        public int operating_system_version { get; set; }
    }
}
