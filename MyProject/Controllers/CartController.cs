using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using MyProject.Filters;
using MyProject.Identity;
using MyProject.Models;

namespace MyProject.Controllers
{
    [MyAuthenFilter]
    public class CartController : Controller
    {
        // GET: Cart
        DBContext db = new DBContext();
        
        public ActionResult NoItem()
        {
            return View();
        }
        public ActionResult Index(string username)
        {
            username = getUsername();
            List<Cart> cart = db.Carts.Where(t => t.userName == username).ToList();
            if(cart.Count == 0)
            {
                return View("NoItem");
            }
            else
            {
                return View(cart);
            }  
        }
        [HttpPost]
        public ActionResult Add(int id = 0) 
        {
            string username = getUsername();
            if (id > 0)
            {
                Product p = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
                Cart cartItem = db.Carts.Where(row => row.ProductId == id && row.userName == username).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    Cart cart = new Cart();
                    cart.ProductId = id;
                    cart.userName = username;
                    cart.Quantity = 1;
                    db.Carts.Add(cart);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", new {username});
        }
        public ActionResult UpdateQuantity(int quan = 0, int proid = 0)
        {
            string username = getUsername();
            if (quan > 0)
            {
                Cart cartItem = db.Carts.Where(cart => cart.ProductId == proid && cart.userName == username).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quan;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", new {username});
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Cart cartItem = db.Carts.Where(t => t.ProductId == id).FirstOrDefault();
            if(cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public string getUsername()
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var usermanager = new AppUserManager(userStore);
            AppUser appUser = usermanager.FindById(User.Identity.GetUserId());
            return (appUser == null) ? "" : appUser.UserName;
        }
    }
}