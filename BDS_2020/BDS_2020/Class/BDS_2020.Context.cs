//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BDS_2020.Class
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDS_2020Entities : DbContext
    {
        public BDS_2020Entities()
            : base("name=BDS_2020Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BD> BDS { get; set; }
        public virtual DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LichSuGiaoDich> LichSuGiaoDiches { get; set; }
        public virtual DbSet<LichSuXem> LichSuXems { get; set; }
        public virtual DbSet<LoaiNha> LoaiNhas { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
    }
}
