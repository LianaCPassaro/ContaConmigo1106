//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContaConmigo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Institution
    {
        public int InstitutionId { get; set; }
        public string InstitutionDescription { get; set; }
        public string InstitutionAdress { get; set; }
        public string PhoneNumber { get; set; }
        public int CityId { get; set; }
    
        public virtual City City { get; set; }
    }
}
