namespace ExamReg.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cathi",
                c => new
                    {
                        CaThiId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        NgayThi = c.DateTime(nullable: false),
                        GioBatDau = c.String(maxLength: 20),
                        GioKetThuc = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.CaThiId);
            
            CreateTable(
                "dbo.lichthi",
                c => new
                    {
                        LichThiId = c.Int(nullable: false, identity: true),
                        LophpId = c.Int(nullable: false),
                        CaThiId = c.Int(nullable: false),
                        PhongThiId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LichThiId)
                .ForeignKey("dbo.cathi", t => t.CaThiId, cascadeDelete: true)
                .ForeignKey("dbo.lophocphan", t => t.LophpId, cascadeDelete: true)
                .ForeignKey("dbo.phongthi", t => t.PhongThiId, cascadeDelete: true)
                .Index(t => t.LophpId)
                .Index(t => t.CaThiId)
                .Index(t => t.PhongThiId);
            
            CreateTable(
                "dbo.lophocphan",
                c => new
                    {
                        LophpId = c.Int(nullable: false, identity: true),
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
                "dbo.sinhvien",
                c => new
                    {
                        SinhVienId = c.Int(nullable: false, identity: true),
                        Mssv = c.Int(nullable: false),
                        FullName = c.String(maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        phone = c.String(maxLength: 15),
                        email = c.String(maxLength: 50),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SinhVienId)
                .ForeignKey("dbo.user", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Role = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.sinhvien_lichthi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SinhVienId = c.Int(nullable: false),
                        LichThiId = c.Int(nullable: false),
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
                        DuDieuKien = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.lophocphan", t => t.LophpId, cascadeDelete: true)
                .ForeignKey("dbo.sinhvien", t => t.SinhVienId, cascadeDelete: true)
                .Index(t => t.SinhVienId)
                .Index(t => t.LophpId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sinhvien_lophp", "SinhVienId", "dbo.sinhvien");
            DropForeignKey("dbo.sinhvien_lophp", "LophpId", "dbo.lophocphan");
            DropForeignKey("dbo.sinhvien_lichthi", "SinhVienId", "dbo.sinhvien");
            DropForeignKey("dbo.sinhvien_lichthi", "LichThiId", "dbo.lichthi");
            DropForeignKey("dbo.sinhvien", "UserId", "dbo.user");
            DropForeignKey("dbo.lichthi", "PhongThiId", "dbo.phongthi");
            DropForeignKey("dbo.lichthi", "LophpId", "dbo.lophocphan");
            DropForeignKey("dbo.lophocphan", "MonThiId", "dbo.monthi");
            DropForeignKey("dbo.lichthi", "CaThiId", "dbo.cathi");
            DropIndex("dbo.sinhvien_lophp", new[] { "LophpId" });
            DropIndex("dbo.sinhvien_lophp", new[] { "SinhVienId" });
            DropIndex("dbo.sinhvien_lichthi", new[] { "LichThiId" });
            DropIndex("dbo.sinhvien_lichthi", new[] { "SinhVienId" });
            DropIndex("dbo.sinhvien", new[] { "UserId" });
            DropIndex("dbo.lophocphan", new[] { "MonThiId" });
            DropIndex("dbo.lichthi", new[] { "PhongThiId" });
            DropIndex("dbo.lichthi", new[] { "CaThiId" });
            DropIndex("dbo.lichthi", new[] { "LophpId" });
            DropTable("dbo.sinhvien_lophp");
            DropTable("dbo.sinhvien_lichthi");
            DropTable("dbo.user");
            DropTable("dbo.sinhvien");
            DropTable("dbo.phongthi");
            DropTable("dbo.monthi");
            DropTable("dbo.lophocphan");
            DropTable("dbo.lichthi");
            DropTable("dbo.cathi");
        }
    }
}
