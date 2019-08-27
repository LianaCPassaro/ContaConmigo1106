using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContaConmigo.Model
{
    public class ProvinceCE
    {
        [Required]
        [Display(Name = "Provincia")]
        public string ProvinceDescription { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public int ProvinceId { get; set; }

    }
}