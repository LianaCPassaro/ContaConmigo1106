﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContaConmigo.Model
{
    public class RequestDonorCE
    {
        [Required]
        [StringLength(50, ErrorMessage = "El {0} debe contener al menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string Name_Request_Don { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El {0} debe contener al menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Apellido")]
        public string Last_Name_Request_Don { get; set; }
        [Required]
        [Display(Name = "Nacimiento")]
        [DataType(DataType.Date)]
        public System.DateTime Birthday { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        public string Phone_Number { get; set; }
        [Required]
        [Display(Name = "Id Provincia")]
        public string ProvinceId { get; set; }
        [Display(Name = "Provincia")]
        public string ProvinceDescription { get; set; }
        [Required]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }
        [Required]
        [Display(Name = "Institución")]
        public int InstitutionId { get; set; }
        [Display(Name = "Ciudad")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Fecha Límite Recepción")]
        [DataType(DataType.Date)]
        public System.DateTime Last_Date_Replacement { get; set; }
        [Required]
        [Display(Name = "Cant. Donantes Requeridos")]
        public int AmountDonor { get; set; }

        [Required]
        [Display(Name = "Grupo y Factor")]
        public int GroupFactorBloodId { get; set; }

        [Display(Name = "Foto")]
        public byte[] Photo { get; set; }
        [Required]
        [Display(Name = "Comentarios")]
        public string Comment { get; set; }

        [Display(Name = "Completo")]
        public string Completed { get; set; }

        [Display(Name = "Institución")]
        public string InstitutionDescription { get; set; }

    }

    [MetadataType(typeof(RequestDonorCE))]
    public partial class RequestDonor //partial se usa para 

    {
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get { return Last_Name_Request_Don + ", " + Name_Request_Don; } }
    }

}