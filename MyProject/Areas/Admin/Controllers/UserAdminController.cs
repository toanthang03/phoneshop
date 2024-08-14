using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Filters;
using MyProject.Identity;

namespace MyProject.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class UserAdminController : Controller
    {
        // GET: Admin/UserAdmin
        AppDBContext db = new AppDBContext();
        public ActionResult ListUser()
        {
            List<AppUser> users = db.Users.ToList();
            return View(users);
        }
    }
}