using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContaConmigo.Model
{
    public class CityCE
    {
        [Required]
        [Display(Name = "Ciudad")]
        public string CityName { get; set; }
        [Required]
        [Display(Name = "Provincias")]
        public string ProvinceId { get; set; }
    }

}