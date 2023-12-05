using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models
{
    public class Client
    {
        [Key]
        public int client_id { get; set; }
        [Required(ErrorMessage = "Имя обязательно для заполнения!")]
        [DisplayName("Name")]
        [MaxLength(50, ErrorMessage = "Имя не может превышать 50 символов!")]

        public string name { get; set; }
        [Required(ErrorMessage = "Фамилия обязательна для заполнения!")]
        [DisplayName("Surname")]
        [MaxLength(50, ErrorMessage = "Фамилия не может превышать 50 символов!")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Номер телефона обязателен для заполнения!")]
        [DisplayName("Phone Number")]
        [MaxLength(18, ErrorMessage = "Номер телефона не может превышать 18 символов!")]
        [RegularExpression(@"^(\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}|\+7\(\d{3}\)\d{3}-\d{2}-\d{2})$", ErrorMessage = "Введите корректный формат номера телефона! Формат: +7 (XXX) XXX-XX-XX")]
        public string phone_number { get; set; }
    }
}
