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
    public class Request_for_repair
    {
        [Key]
        public int request_id { get; set; }


        [Required(ErrorMessage = "Выбор модели телефона обязателен!")]
        public int model_id { get; set; }
        [ForeignKey("model_id")]
        [ValidateNever]
        public Phone_model Phone_Model_of_request_for_repair { get; set; }


        [Required(ErrorMessage = "Выбор услуги обязателен!")]
        public int service_id { get; set; }
        [ForeignKey("service_id")]
        [ValidateNever]
        public Service Service_of_request_for_repair { get; set; }


        [Required(ErrorMessage = "Выбор клиента обязателен!")]
        public int client_id { get; set; }
        [ForeignKey("client_id")]
        [ValidateNever]
        public Client Client_of_request_for_repair { get; set; }



        [ValidateNever]
        [DisplayName("Datetime of request")]
        public DateTime? datetime_of_request { get; set; }

        [ValidateNever]
        [DisplayName("Status")]
        public string? status { get; set; }

    }
}
