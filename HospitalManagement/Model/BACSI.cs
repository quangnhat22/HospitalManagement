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
    
    public partial class BACSI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BACSI()
        {
            this.BACSILIENQUANs = new HashSet<BACSILIENQUAN>();
            this.LATRUONGTO = new HashSet<TO>();
        }
    
        public string CMND_CCCD { get; set; }
        public string HO { get; set; }
        public string TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string QUOCTICH { get; set; }
        public string DIACHI { get; set; }
        public Nullable<System.DateTime> NGSINH { get; set; }
        public Nullable<bool> GIOITINH { get; set; }
        public string VAITRO { get; set; }
        public string CHUYENKHOA { get; set; }
        public string GHICHU { get; set; }
        public Nullable<int> IDTO { get; set; }
    
        public virtual TO TO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BACSILIENQUAN> BACSILIENQUANs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TO> LATRUONGTO { get; set; }
    }
}
