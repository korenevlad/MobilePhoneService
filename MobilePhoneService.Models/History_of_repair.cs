using Microsoft.AspNetCore.Mvc;
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
    public class History_of_repair
    {
        [Key]
        public int history_id { get; set; }


        [DisplayName("Date start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? date_start { get; set; }



        [DisplayName("Date end")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? date_end { get; set; }


        [Required]
        public int request_id { get; set; }
        [ForeignKey("request_id")]
        [ValidateNever]
        public Request_for_repair Request_for_repair_of_history_of_repair { get; set; }
    }
}
