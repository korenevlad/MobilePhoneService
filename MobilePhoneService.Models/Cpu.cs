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

        [Required(ErrorMessage = "Название обязательно для заполнения!")]
        [DisplayName("Model")]
        [MaxLength(50)]
        public string model { get; set; }


        public enum AmountCernelsOptions
        {
            [Display(Name = "4")]
            AmountCernels4 = 4,

            [Display(Name = "6")]
            AmountCernels6 = 6,

            [Display(Name = "8")]
            AmountCernels8 = 8,
        }
        [Required(ErrorMessage = "Количестов ядер обязательно для заполнения!")]
        [DisplayName("Amount cernels")]
        [EnumDataType(typeof(AmountCernelsOptions), ErrorMessage = "Допустимые значения для количества ядер: 4, 6 или 8!")]
        public int amount_cernels { get; set; }



        [Required(ErrorMessage = "Частота обязательна для заполнения!")]
        [DisplayName("Frequency")]
        [Range(1, 3, ErrorMessage = "Частота может быть в диапозоне от 1 до 3! Выберете ближайшее целое число!")]
        public int frequency { get; set; }
    }
}
