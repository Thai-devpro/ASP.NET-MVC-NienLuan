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
    
    public partial class TAIKHOAN_MTQ
    {
        public int MA_MTQ { get; set; }
        public string MATKHAU_MTQ { get; set; }
        public string TAIKHOAN { get; set; }
    
        public virtual MANHTHUONGQUAN MANHTHUONGQUAN { get; set; }
    }
}
