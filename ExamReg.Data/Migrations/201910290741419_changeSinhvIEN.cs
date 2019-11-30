namespace ExamReg.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSinhvIEN : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sinhvien", "Address", c => c.String(maxLength: 256));
            AlterColumn("dbo.sinhvien", "MSSV", c => c.String(maxLength: 8));
            AlterColumn("dbo.sinhvien", "FullName", c => c.String(maxLength: 256));
            AlterColumn("dbo.sinhvien", "BirthDay", c => c.DateTime());
            DropColumn("dbo.ApplicationUsers", "MSSV");
            DropColumn("dbo.ApplicationUsers", "FullName");
            DropColumn("dbo.ApplicationUsers", "Address");
            DropColumn("dbo.ApplicationUsers", "BirthDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "BirthDay", c => c.DateTime());
            AddColumn("dbo.ApplicationUsers", "Address", c => c.String(maxLength: 256));
            AddColumn("dbo.ApplicationUsers", "FullName", c => c.String(maxLength: 256));
            AddColumn("dbo.ApplicationUsers", "MSSV", c => c.String(maxLength: 8));
            AlterColumn("dbo.sinhvien", "BirthDay", c => c.DateTime(nullable: false));
            AlterColumn("dbo.sinhvien", "FullName", c => c.String(maxLength: 50));
            AlterColumn("dbo.sinhvien", "MSSV", c => c.Int(nullable: false));
            DropColumn("dbo.sinhvien", "Address");
        }
    }
}
