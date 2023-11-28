using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models
{
    public class Manufacturer
    {
        [Key]
        public int manufacturer_id { get; set; }

        [Required(ErrorMessage = "Название обязательно для заполнения!")]
        [DisplayName("Name")]
        [MaxLength(50)]
        public string manufacturer_name { get; set; }

        [ValidateNever]
        [DisplayName("Country")]
        [MaxLength(50)]
        public string? country { get; set; }
    }
}
