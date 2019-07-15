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
        [StringLength(50, ErrorMessage = "El {0} debe contener al menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nombres")]
        public string Name_Request_Don { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El {0} debe contener al menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Apellido")]
        public string Last_Name_Request_Don { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public string Birthday { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        public string Phone_Number { get; set; }
        [Required]
        [Display(Name = "Provincia")]
        public int ProvinceId { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "Fecha Límite Recepción")]
        [DataType(DataType.Date)]
        public System.DateTime Last_Date_Replacement { get; set; }
        [Required]
        [Display(Name = "Cant. Donantes Requeridos")]
        public int AmountDonor { get; set; }
        [Required]
        [Display(Name = "Institución")]
        public int InstitutionId { get; set; }
        [Required]
        [Display(Name = "Grupo Sanguíneo")]
        public int GroupId { get; set; }
        [Required]
        [Display(Name = "Factor Sanguíneo")]
        public int FactorId { get; set; }

        [Display(Name = "Foto")]
        public int Photo { get; set; }
        [Display(Name = "Comentarios")]
        public string Comment { get; set; }
    }

    [MetadataType(typeof(RequestDonorCE))]
    public partial class RequestDonor //partial se usa para 

    {
    [Required]
    [Display(Name = "Nombre Completo")]
    public string NombreCompleto { get { return Last_Name_Request_Don + ", " + Name_Request_Don; } }

    }
}