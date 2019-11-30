namespace ExamReg.Data.Migrations
{
    using ExamReg.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExamReg.Data.ExamRegDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ExamReg.Data.ExamRegDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data.
			//var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ExamRegDbContext()));

			//var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ExamRegDbContext()));

			//var user = new ApplicationUser()
			//{
			//	UserName = "ducnha",
			//	Email = "17020940@vnu.edu.vn"
			//};
			//var user = new ApplicationUser()
			//{
			//	UserName = "nctuong",
			//	Email = "17021120@vnu.edu.vn"
			//};

			//manager.Create(user, "123456$");
			
			//var adminUser = manager.FindByEmail("17021120@vnu.edu.vn");
			

			//manager.AddToRoles(adminUser.Id, "User");


		}
    }
}
