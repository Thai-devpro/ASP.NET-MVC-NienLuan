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
    
    public partial class TT_QUYENGOP_HIENKIM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TT_QUYENGOP_HIENKIM()
        {
            this.TT_QUYDOI = new HashSet<TT_QUYDOI>();
        }
    
        public int MA_QGHK { get; set; }
        public Nullable<int> MA_CD { get; set; }
        public Nullable<int> MA_MTQ { get; set; }
        public Nullable<int> SOTIEN { get; set; }
        public string THOIGIANNHAN_HK { get; set; }
        public string TRANGTHAI_HK { get; set; }
    
        public virtual CHIENDICH CHIENDICH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TT_QUYDOI> TT_QUYDOI { get; set; }
        public virtual MANHTHUONGQUAN MANHTHUONGQUAN { get; set; }
    }
}