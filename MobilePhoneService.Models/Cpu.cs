using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.Models
{
    public class Cpu
    {
        [Key]
        public int cpu_id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Model")]
        public string model { get; set; }

        [Required]
        [Range(2, 16, ErrorMessage = "Количество ядер может быть в диапозоне от 2 до 16!")]
        [DisplayName("Amount cernels")]
        public int amount_cernels { get; set; }


        [Required]
        [Range(1, 3, ErrorMessage = "Частота может быть в диапозоне от 1 до 3! Выберете ближайшее целое число!")]
        [DisplayName("Frequency")]
        public int frequency { get; set; }
    }
}
