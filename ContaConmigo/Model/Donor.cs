//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContaConmigo.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Donor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donor()
        {
            this.DonorRequestDonors = new HashSet<DonorRequestDonor>();
        }

        public int DonorId { get; set; }
        public string Name_Don { get; set; }
        public string Last_Name_Don { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Nullable<System.DateTime> Last_Date_Blood_Extract { get; set; }
        public int BloodGroupFactorId { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceDescription { get; set; }


        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonorRequestDonor> DonorRequestDonors { get; set; }
        public virtual GroupFactorBlood GroupFactorBlood { get; set; }
    }

}
