//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TANG()
        {
            this.PHONGs = new HashSet<PHONG>();
            this.TOes = new HashSet<TO>();
        }
    
        public int ID { get; set; }
        public Nullable<int> SOTANG { get; set; }
        public Nullable<int> SLPHONG { get; set; }
        public Nullable<int> IDTOA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHONG> PHONGs { get; set; }
        public virtual TOA TOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TO> TOes { get; set; }
    }
}
