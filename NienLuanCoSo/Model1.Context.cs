﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NIENLUANCOSOEntities4 : DbContext
    {
        public NIENLUANCOSOEntities4()
            : base("name=NIENLUANCOSOEntities4")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CHIENDICH> CHIENDICHes { get; set; }
        public virtual DbSet<HIEN_VAT> HIEN_VAT { get; set; }
        public virtual DbSet<LOAI_HV> LOAI_HV { get; set; }
        public virtual DbSet<MANHTHUONGQUAN> MANHTHUONGQUANs { get; set; }
        public virtual DbSet<NOIHOTRO> NOIHOTROes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TAIKHOAN_ADMIN> TAIKHOAN_ADMIN { get; set; }
        public virtual DbSet<TAIKHOAN_MTQ> TAIKHOAN_MTQ { get; set; }
        public virtual DbSet<TT_QUYENGOP_HIENVAT> TT_QUYENGOP_HIENVAT { get; set; }
        public virtual DbSet<TT_TRAOTANG> TT_TRAOTANG { get; set; }
        public virtual DbSet<CHUCNANG> CHUCNANGs { get; set; }
        public virtual DbSet<CHUCVU> CHUCVUs { get; set; }
        public virtual DbSet<THANHVIEN> THANHVIENs { get; set; }
    }
}
