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

        [Required]
        [MaxLength(50)]
        [DisplayName("Name")]
        public string manufacturer_name { get; set; }

        [MaxLength(50)]
        [DisplayName("Country")]
        [ValidateNever]
        public string? country { get; set; }
    }
}
