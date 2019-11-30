using System;
using System.Collections.Generic;
using ExamReg.Model.Models;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExamReg.Data
{
    public class ExamRegDbContext : IdentityDbContext<ApplicationUser>
    {
        public ExamRegDbContext() : base("ExamRegConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<CaThi> Cathi { set; get; }
        public DbSet<LichThi> lichThi { set; get; }
        public DbSet<PhongThi> PhongThi { set; get; }
        public DbSet<SinhVien> SinhVien { set; get; }
        public DbSet<MonThi> MonThi { set; get; }
        public DbSet<LopHocPhan> LopHocPhan { set; get; }
        public DbSet<SinhVienLophp> SinhVienLophp { set; get; }
        public DbSet<SinhVienLichThi> SinhVienLichThi { set; get; }


        public static ExamRegDbContext Create()
        {
            return new ExamRegDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }
    }
}
