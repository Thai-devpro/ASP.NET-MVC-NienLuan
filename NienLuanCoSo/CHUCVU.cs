//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NienLuanCoSo
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHUCVU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUCVU()
        {
            this.THANHVIENs = new HashSet<THANHVIEN>();
            this.CHUCNANGs = new HashSet<CHUCNANG>();
        }
    
        public int MA_CV { get; set; }
        public string TEN_CV { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THANHVIEN> THANHVIENs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUCNANG> CHUCNANGs { get; set; }
    }
}
