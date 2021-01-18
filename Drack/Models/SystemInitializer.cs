using Drack.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class SystemInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)

        {

            var UserManager = new UserManager<ApplicationUser>(new

                                           UserStore<ApplicationUser>(context));

            var RoleManager = new RoleManager<IdentityRole>(new

                                     RoleStore<IdentityRole>(context));



            //Create Roles if it does not exist

            if (!RoleManager.RoleExists("Developer"))

            {

                var roleresult = RoleManager.Create(new IdentityRole("Developer"));

            }

            if (!RoleManager.RoleExists("Admin"))

            {

                var roleresult = RoleManager.Create(new IdentityRole("Admin"));

            }

            //Drack User
            string SuperEmail = "developer@drack.dev";

            //Add developer Admin

            var dev = new ApplicationUser();

            dev.Email = SuperEmail;
            dev.UserName = SuperEmail;
            dev.FullName = "System Developer";
            dev.EmailConfirmed = true;

            var devresult = UserManager.Create(dev, "Dr4ck32"); //#User123

            //Add Developer to Role Admin and Auditor

            if (devresult.Succeeded)

            {

                var result1 = UserManager.AddToRole(dev.Id, "Admin");
                var result2 = UserManager.AddToRole(dev.Id, "Developer");

            }

            base.Seed(context);

        }
    }
}