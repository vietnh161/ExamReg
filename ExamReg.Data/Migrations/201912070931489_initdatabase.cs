namespace ExamReg.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cathi",
                c => new
                    {
                        CaThiId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CaThiId);
            
            CreateTable(
                "dbo.kithi",
                c => new
                    {
                        KiThiId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.KiThiId);
            
            CreateTable(
                "dbo.lichthi",
                c => new
                    {
                        LichThiId = c.Int(nullable: false, identity: true),
                        LophpId = c.Int(nullable: false),
                        CaThiId = c.Int(nullable: false),
                        PhongThiId = c.Int(nullable: false),
                        NgayThi = c.DateTime(nullable: false),
                        GioBatDau = c.Time(nullable: false, precision: 7),
                        GioKetThuc = c.Time(nullable: false, precision: 7),
                        Status = c.Boolean(nullable: false),
                        Count = c.Int(nullable: false),
                        KiThiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LichThiId)
                .ForeignKey("dbo.cathi", t => t.CaThiId, cascadeDelete: true)
                .ForeignKey("dbo.kithi", t => t.KiThiId, cascadeDelete: true)
                .ForeignKey("dbo.lophocphan", t => t.LophpId, cascadeDelete: true)
                .ForeignKey("dbo.phongthi", t => t.PhongThiId, cascadeDelete: true)
                .Index(t => t.LophpId)
                .Index(t => t.CaThiId)
                .Index(t => t.PhongThiId)
                .Index(t => t.KiThiId);
            
            CreateTable(
                "dbo.lophocphan",
                c => new
                    {
                        LophpId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 20),
                        Name = c.String(maxLength: 50),
                        MonThiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LophpId)
                .ForeignKey("dbo.monthi", t => t.MonThiId, cascadeDelete: true)
                .Index(t => t.MonThiId);
            
            CreateTable(
                "dbo.monthi",
                c => new
                    {
                        MonThiId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 20),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MonThiId);
            
            CreateTable(
                "dbo.phongthi",
                c => new
                    {
                        PhongThiId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        SoLuongMay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhongThiId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.sinhvien",
                c => new
                    {
                        SinhVienId = c.Int(nullable: false, identity: true),
                        MSSV = c.String(maxLength: 8),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        BirthDay = c.DateTime(),
                        phone = c.String(maxLength: 15),
                        email = c.String(maxLength: 50),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SinhVienId)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 256),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.sinhvien_lichthi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SinhVienId = c.Int(nullable: false),
                        LichThiId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.lichthi", t => t.LichThiId, cascadeDelete: true)
                .ForeignKey("dbo.sinhvien", t => t.SinhVienId, cascadeDelete: true)
                .Index(t => t.SinhVienId)
                .Index(t => t.LichThiId);
            
            CreateTable(
                "dbo.sinhvien_lophp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SinhVienId = c.Int(nullable: false),
                        LophpId = c.Int(nullable: false),
                        KiThiId = c.Int(nullable: false),
                        DuDieuKien = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.kithi", t => t.KiThiId, cascadeDelete: true)
                .ForeignKey("dbo.lophocphan", t => t.LophpId, cascadeDelete: true)
                .ForeignKey("dbo.sinhvien", t => t.SinhVienId, cascadeDelete: true)
                .Index(t => t.SinhVienId)
                .Index(t => t.LophpId)
                .Index(t => t.KiThiId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sinhvien_lophp", "SinhVienId", "dbo.sinhvien");
            DropForeignKey("dbo.sinhvien_lophp", "LophpId", "dbo.lophocphan");
            DropForeignKey("dbo.sinhvien_lophp", "KiThiId", "dbo.kithi");
            DropForeignKey("dbo.sinhvien_lichthi", "SinhVienId", "dbo.sinhvien");
            DropForeignKey("dbo.sinhvien_lichthi", "LichThiId", "dbo.lichthi");
            DropForeignKey("dbo.sinhvien", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.lichthi", "PhongThiId", "dbo.phongthi");
            DropForeignKey("dbo.lichthi", "LophpId", "dbo.lophocphan");
            DropForeignKey("dbo.lophocphan", "MonThiId", "dbo.monthi");
            DropForeignKey("dbo.lichthi", "KiThiId", "dbo.kithi");
            DropForeignKey("dbo.lichthi", "CaThiId", "dbo.cathi");
            DropIndex("dbo.sinhvien_lophp", new[] { "KiThiId" });
            DropIndex("dbo.sinhvien_lophp", new[] { "LophpId" });
            DropIndex("dbo.sinhvien_lophp", new[] { "SinhVienId" });
            DropIndex("dbo.sinhvien_lichthi", new[] { "LichThiId" });
            DropIndex("dbo.sinhvien_lichthi", new[] { "SinhVienId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.sinhvien", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.lophocphan", new[] { "MonThiId" });
            DropIndex("dbo.lichthi", new[] { "KiThiId" });
            DropIndex("dbo.lichthi", new[] { "PhongThiId" });
            DropIndex("dbo.lichthi", new[] { "CaThiId" });
            DropIndex("dbo.lichthi", new[] { "LophpId" });
            DropTable("dbo.sinhvien_lophp");
            DropTable("dbo.sinhvien_lichthi");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.sinhvien");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.phongthi");
            DropTable("dbo.monthi");
            DropTable("dbo.lophocphan");
            DropTable("dbo.lichthi");
            DropTable("dbo.kithi");
            DropTable("dbo.cathi");
        }
    }
}
