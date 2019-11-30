namespace ExamReg.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deluser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.sinhvien", "UserId", "dbo.user");
            DropIndex("dbo.sinhvien", new[] { "UserId" });
            DropColumn("dbo.sinhvien", "UserId");
            DropTable("dbo.user");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.sinhvien", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.sinhvien", "UserId");
            AddForeignKey("dbo.sinhvien", "UserId", "dbo.user", "UserId", cascadeDelete: true);
        }
    }
}
