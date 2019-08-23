using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContaConmigo.Model
{
    public class UserContaConmigoCE
    {
        [Required]
        [StringLength(50, ErrorMessage = "El {0} debe contener al menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string UserFirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El {0} debe contener al menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Apellido")]
        public string UserLastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debes ingresar tu email.")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirme el Email")]
        [Required(ErrorMessage = "La confirmación de email es requerida.")]
        [Compare("EmailAddress", ErrorMessage = "El email y la confirmación del email no coinciden")]
        public string ConfirmEmailAddress { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Debes ingresar una contraseña")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El {0} debe contener al menos {2} caracteres.")]             
        public string PasswordUser { get; set; }

        [Display(Name = "Confirmar la Contraseña")]
        [Required(ErrorMessage = "La confirmación de la contraseña es requerida")]
        [DataType(DataType.Password)]
        [Compare("PasswordUser", ErrorMessage = "La contraseña y la confirmación de la contraseña no coinciden.")]
        public string ConfirmPasswordUser { get; set; }
    }
    [MetadataType(typeof(UserContaConmigoCE))]
    public partial class UserContaConmigo //partial se usa para 

    {
        [Required]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get { return UserFirstName + ", " + UserLastName; } }
    }
}