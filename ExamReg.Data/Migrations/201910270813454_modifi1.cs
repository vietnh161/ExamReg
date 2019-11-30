namespace ExamReg.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifi1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.lichthi", "NgayThi", c => c.DateTime(nullable: false));
            AddColumn("dbo.lichthi", "GioBatDau", c => c.String(maxLength: 20));
            AddColumn("dbo.lichthi", "GioKetThuc", c => c.String(maxLength: 20));
            DropColumn("dbo.cathi", "NgayThi");
            DropColumn("dbo.cathi", "GioBatDau");
            DropColumn("dbo.cathi", "GioKetThuc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.cathi", "GioKetThuc", c => c.String(maxLength: 20));
            AddColumn("dbo.cathi", "GioBatDau", c => c.String(maxLength: 20));
            AddColumn("dbo.cathi", "NgayThi", c => c.DateTime(nullable: false));
            DropColumn("dbo.lichthi", "GioKetThuc");
            DropColumn("dbo.lichthi", "GioBatDau");
            DropColumn("dbo.lichthi", "NgayThi");
        }
    }
}
