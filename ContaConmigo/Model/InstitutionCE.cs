using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContaConmigo.Model
{
    public class InstitutionCE
    {
        [Required]
        [Display(Name = "Institución")]
        public string InstitutionDescription { get; set; }

        [Required]
        [Display(Name = "Institución")]
        public int InstitutionId { get; set; }
    }
}