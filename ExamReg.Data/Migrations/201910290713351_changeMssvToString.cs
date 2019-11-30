namespace ExamReg.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMssvToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "MSSV", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "MSSV", c => c.Int(nullable: false));
        }
    }
}
