using MyProject.Filters;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class CategoryAdminController : Controller
    {
        // GET: Admin/CategoryAdmin
        DBContext db = new DBContext();
        public ActionResult Loai()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }
    }
}