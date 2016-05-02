using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Graduation.Web.Models
{
    //public class Initializer: DropCreateDatabaseAlways<ApplicationDbContext>
    //{
    //    protected override void Seed(ApplicationDbContext context)
    //    {
    //        var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

    //        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

    //        // создаем две роли
    //        var adminRole = new IdentityRole { Name = "admin" };
    //        var userRole = new IdentityRole { Name = "user" };

    //        // добавляем роли в бд
    //        roleManager.Create(adminRole);
    //        roleManager.Create(userRole);

    //        // создаем пользователей
    //        var admin = new ApplicationUser { Email = "Romat10@ukr.net", UserName = "Romat10@ukr.net" };

    //        string password = "ad46D_ewr3";
    //        var result = userManager.Create(admin, password);

    //        // если создание пользователя прошло успешно
    //        if (result.Succeeded)
    //        {
    //            // добавляем для пользователя роль
    //            userManager.AddToRole(admin.Id, adminRole.Name);
    //            userManager.AddToRole(admin.Id, userRole.Name);
    //        }

    //        base.Seed(context);
    //    }
    //}
}
