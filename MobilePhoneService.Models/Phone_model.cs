using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models
{
    public class Phone_model
    {
        [Key]
        public int model_id { get; set; }


        [Required(ErrorMessage = "Название обязательно для заполнения!")]
        [DisplayName("Name")]
        [MaxLength(50)]
        public string name { get; set; }


        [DisplayName("Year of release")]
        [Range(2010, 2023, ErrorMessage = "Год выпуска может быть в диапозоне от 2010 до 2023!")]
        public int? year_of_release { get; set; }


        [Required(ErrorMessage = "Выбор производителя обязателен!")]
        public int manufacturer_id { get; set; }

        [ForeignKey("manufacturer_id")]
        [ValidateNever]
        public Manufacturer Manufacturer_of_phone_model { get; set; }


        [Required(ErrorMessage = "Выбор спецификации обязателен!")]
        public int specification_id { get; set; }

        [ForeignKey("specification_id")]
        [ValidateNever]
        public Phone_specification Phone_Specification_of_phone_model { get; set; }
    }
}
