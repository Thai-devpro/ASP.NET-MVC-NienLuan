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
    
    public partial class TT_TRAOTANG
    {
        public int MA_TT { get; set; }
        public int MANOI { get; set; }
        public int MA_HV { get; set; }
        public int MA_CD { get; set; }
        public int SOLUONG_TT { get; set; }
        public System.DateTime NGAYTANG { get; set; }
        public string ANH_TT { get; set; }
    
        public virtual CHIENDICH CHIENDICH { get; set; }
        public virtual HIEN_VAT HIEN_VAT { get; set; }
        public virtual NOIHOTRO NOIHOTRO { get; set; }
    }
}
