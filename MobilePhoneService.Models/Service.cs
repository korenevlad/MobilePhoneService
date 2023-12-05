using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models
{
    public class Service
    {
        [Key]
        public int service_id { get; set; }

        [Required(ErrorMessage = "Название обязательно для заполнения!")]
        [DisplayName("Name")]
        [MaxLength(50, ErrorMessage = "Название не может превышать 50 символов!")]
        public string description { get; set; }

        [Required(ErrorMessage = "Цена обязательна для заполнения!")]
        [Range(300, 20000, ErrorMessage = "Цена может быть в диапозоне от 300 до 20000!")]
        public int price { get; set; }
    }
}
