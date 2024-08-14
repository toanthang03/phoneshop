using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBContext db = new DBContext();
        public ActionResult Index(int id = 1, string Search = "", string Sort="Tang", int page = 1)
        {
            List<Product> p = db.Products.Where(t => t.ProductName.Contains(Search)).ToList();
            
            //paging
            //ViewBag.IconClass = IconClass;
            ViewBag.SortColumn = Sort;
            switch (Sort)
            {
                case "Tang":
                    p = p.OrderBy(t => t.Price).ToList();
                    break;
                case "Giam":
                    p = p.OrderByDescending(t => t.Price).ToList();
                    break;
                default:
                    break;
            }

            int NoOfRecordPerPage = 12;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.Count) / Convert.ToDouble(NoOfRecordPerPage)));

            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            p = p.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(p);
        }
        public ActionResult ListProduct(int id = 1, string Sort = "Tang", int page = 1)
        {
            List<Product> p = db.Products.ToList().Where(t => t.CategoryID == id).ToList();
            Category c = db.Categories.ToList().Where(t => t.CategoryID == id).FirstOrDefault();
            ViewBag.category = c.CategoryName;
            ViewBag.ID = id;
            ViewBag.SortColumn = Sort;
            //Sort
            switch (Sort)
            {
                case "Tang":
                    p = p.OrderBy(t => t.Price).ToList();
                    break;
                case "Giam":
                    p = p.OrderByDescending(t => t.Price).ToList();
                    break;
                default:
                    break;
            }
            int NoOfRecordPerPage = 4;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.Count) / Convert.ToDouble(NoOfRecordPerPage)));

            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            p = p.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(p);
        }
        public ActionResult Detail(int? id)
        {
            List<Product> p = db.Products.ToList();
            Product products = null;
            foreach (Product pro in p)
            {
                if (pro.ProductID == id)
                    products = pro;
            }
            ViewBag.Product = products;
            return View(p);
        }
    }
}