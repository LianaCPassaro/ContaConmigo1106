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
        //[DataType(DataType.Date)] Try removing [DataType(DataType.Date)] because I believe this creates <input type="date" />. If you do that you'll end up with a <input type="text" /> to which you can attach jQuery date-picker.
        [Display(Name = "Última Extracción")]
        [DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Last_Date_Blood_Extract { get; set; }

        [Required]
        [Display(Name = "Grupo/Factor")]
        public int BloodGroupFactorId { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Fecha Modificación")]
        public DateTime UpdatedDate { get; set; }
    }
    [MetadataType(typeof(DonorCE))]
    public partial class Donor
    {
        [Display(Name = "Nombre Completo")]
        public string NombreCompletoDon { get { return Last_Name_Don + ", " + Name_Don; } }
    }
}