using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MyProject.Filters;
using MyProject.Models;
using static System.Net.WebRequestMethods;

namespace MyProject.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductAdminController : Controller
    {
        // GET: Admin/ProductAdmin
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            List<Product> p = db.Products.ToList();            
            return View(p);
        }

        public ActionResult ThemSP()
        {
            List<Category> category = db.Categories.ToList();
            ViewBag.category = category;
            return View();
        }
        [HttpPost]
        public ActionResult ThemSP(Product product, HttpPostedFileBase imgFile)
        {
            if(ModelState.IsValid)
            {
                if(imgFile != null && imgFile.ContentLength > 0) 
                {
                    if(imgFile.ContentLength < 2000000)
                    {
                        var kiemtra = new[] { ".jpg", ".png" };
                        var fileEx = Path.GetExtension(imgFile.FileName).ToLower();
                        if (!kiemtra.Contains(fileEx))
                        {
                            ModelState.AddModelError("ProductImage", "Chỉ chấp nhận hình ảnh dạng JPG hoặc PNG.");
                            return View();
                        }
                        product.ProductImage = imgFile.FileName.ToString();
                        db.Products.Add(product);
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Kích thước file không được lớn hơn 2MB.");
                        return View();
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            Product pro = db.Products.Where(t =>t.ProductID == id).FirstOrDefault();
            List<Category> cate = db.Categories.ToList();
            ViewBag.category = cate;
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(Product p, HttpPostedFileBase imgFile)
        {
            Product product = db.Products.Where(t => t.ProductID == p.ProductID).FirstOrDefault();
            if(ModelState.IsValid)
            {
                if(imgFile != null && imgFile.ContentLength > 0) 
                {
                    if(imgFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("ProductImage", "Hình ảnh không được lớn hơn 2MB");
                        return View(product);
                    }
                    var kiemtraAnh = new[] { "jpg", "png" };
                    var fileEx = Path.GetExtension(imgFile.FileName).ToLower();
                    if(!kiemtraAnh.Contains(fileEx))
                    {
                        ModelState.AddModelError("ProductImage", "Hình ảnh chỉ chấp nhận đuôi .png và .jpg");
                        return View(product);
                    }
                    string imagePath = Server.MapPath("~/image/" + product.ProductImage);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    product.ProductName = p.ProductName;
                    product.ProductImage = p.ProductImage;
                    product.Price = p.Price;
                    product.Description = p.Description;
                    product.CategoryID = p.CategoryID;
                    db.SaveChanges();
                    var fileName = product.ProductID.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/image"), fileName);
                    imgFile.SaveAs(path);
                    product.ProductImage = fileName;
                    db.SaveChanges();
                }
                else
                {
                    product.ProductName = p.ProductName;
                    product.Price = p.Price;
                    product.Description = p.Description;
                    product.CategoryID = p.CategoryID;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        } 
        public ActionResult Delete(int id)
        {
            Product pro = db.Products.Where(t => t.ProductID == id).FirstOrDefault();
            ViewBag.category = db.Categories.ToList();
            return View(pro);
        }

        [HttpPost]
        public ActionResult Delete(Product pro)
        {
            Product p = db.Products.Where(t => t.ProductID == pro.ProductID).FirstOrDefault();
            if (p != null)
            {
                if (p.ProductImage == "")
                {
                    db.Products.Remove(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                string imagePath = Server.MapPath("~/image/" + p.ProductImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                p.ProductImage = "";
                db.Products.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}