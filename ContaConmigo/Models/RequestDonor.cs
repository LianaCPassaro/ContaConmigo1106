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
    
    public partial class RequestDonor
    {
        public int RequestDonorId { get; set; }
        public string Name_Request_Don { get; set; }
        public string Last_Name_Request_Don { get; set; }
        public int CityId { get; set; }
        public System.DateTime Last_Date_Replacement { get; set; }
        public int AmountDonor { get; set; }
        public int InstitutionId { get; set; }
        public int GroupId { get; set; }
        public int FactorId { get; set; }
        public string Comment { get; set; }
        public string Phone_Number { get; set; }
        public System.DateTime Birthday { get; set; }
        public Nullable<bool> Completed { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual BloodFactor BloodFactor { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public virtual City City { get; set; }
    }
}
