using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContaConmigo.Model
{
    public class DonorCE
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Name_Don { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string Last_Name_Don { get; set; }
        [Required]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }
        [Required]
        [Display(Name = "Provincia")]
        public int ProvinceId { get; set; }
        [Required]
        [Display(Name = "Fecha Ult. Extracción")]
        [DataType(DataType.Date)]
        public System.DateTime Last_Date_Blood_Extract { get; set; }
        [Required]
        [Display(Name = "Grupo/Factor")]
        public int BloodGroupFactorId { get; set; }

        public string BloodGroupFactorDescription { get; set; }

        //public int Id { get; set; }
        public string CityName { get; set; }

        public virtual City City { get; set; }
    }
        [MetadataType(typeof(DonorCE))]
        public partial class Donor       
        {
            [Display(Name = "Nombre Completo")]
            public string NombreCompletoDon { get { return Last_Name_Don + ", " + Name_Don; } }
        }
}