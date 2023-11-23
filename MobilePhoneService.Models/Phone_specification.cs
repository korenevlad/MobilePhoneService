using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MobilePhoneService.Models.Cpu;

namespace MobilePhoneService.Models
{
    public class Phone_specification
    {
        [Key]
        public int specification_id { get; set; }

        public enum RamOptions
        {
            [Display(Name = "4")]
            Ram4 = 4,
            [Display(Name = "6")]
            Ram6 = 6,
            [Display(Name = "8")]
            Ram8 = 8,
            [Display(Name = "12")]
            Ram12 = 12,
        }
        [Required(ErrorMessage = "RAM обязательно для заполнения!")]
        [DisplayName("RAM")]
        [EnumDataType(typeof(RamOptions), ErrorMessage = "Допустимые значения для RAM: 4, 6 8 или 12!")]
        public int ram { get; set; }

        public enum InternalMemoryOptions
        {
            [Display(Name = "32")]
            IntMem32 = 32,
            [Display(Name = "64")]
            IntMem64 = 64,
            [Display(Name = "128")]
            IntMem128 = 128,
            [Display(Name = "256")]
            IntMem256 = 256,
            [Display(Name = "512")]
            IntMem512 = 512,
            [Display(Name = "1024")]
            IntMem1024 = 1024,
        }

        [Required(ErrorMessage = "Внутренняя память обязательна для заполнения!")]
        [DisplayName("Внутренняя память")]
        [EnumDataType(typeof(InternalMemoryOptions), ErrorMessage = "Допустимые значения для внутренней памяти: 32, 64, 128, 256, 512 или 1024!")]
        public int internal_memory { get; set; }

        [Required(ErrorMessage = "Размер экрана обязателен для заполнения!")]
        [DisplayName("Размер экрана")]
        [Range(5, 8, ErrorMessage = "Размер экрана может быть в диапозоне от 5 до 8 дюймов! Выберете ближайшее целое число!")]
        public int screen_size { get; set; }



        [Required(ErrorMessage = "Выбор процессора обязателен!")]
        public int cpu_id { get; set; }

        [ForeignKey("cpu_id")]
        [ValidateNever]
        public Cpu Cpu_of_specification { get; set; }



        [Required(ErrorMessage = "Выбор операционной системы обязателен!")]
        public int operating_system_id { get; set; }

        [ForeignKey("operating_system_id")]
        [ValidateNever]
        public Operating_system Operating_system_of_specification { get; set; }

    }
}
