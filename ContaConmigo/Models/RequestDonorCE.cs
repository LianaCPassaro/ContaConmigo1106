using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContaConmigo.Models
{
    public class RequestDonorCE
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Name_Request_Don { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string Last_Name_Request_Don { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        public string Phone_Number { get; set; }
        [Required]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }
        [Required]
        [Display(Name = "Código Postal")]
        public int ZipCode { get; set; }
        [Required]
        [Display(Name = "Institución")]
        public int InstitutionId { get; set; }
        [Required]
        [Display(Name = "Fecha Límite Recepción")]
        public System.DateTime Last_Date_Replacement { get; set; }
        [Required]
        [Display(Name = "Cant. Donantes Requeridos")]
        public int AmountDonor { get; set; }
        [Required]
        [Display(Name = "Grupo Sanguíneo")]
        public int BloodGroupId { get; set; }
        [Required]
        [Display(Name = "Factor Sanguíneo")]
        public int BloodFactorId { get; set; }
        [Display(Name = "Comentarios")]
        public string Comments { get; set; }
    }
    [MetadataType(typeof(RequestDonorCE))]

        public partial class RequestDonor //partial se usa para 

        {
        [Required]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get { return Last_Name_Request_Don + ", " + Name_Request_Don; } }

        //[Required]
        //[Display(Name = "Localidad")]
        //public string CiudadCodificada { get {
        //        List<City> ciudad = new List<City>();
        //        return Last_Name_Request_Don + ", " + Name_Request_Don; } }
    }
}