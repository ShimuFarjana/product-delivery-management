using ProductDeliveryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using X.PagedList.Mvc;


namespace ProductDeliveryManagement.Controllers
{
    public class ProductsController : Controller
    {
        DeliveryManagementDbContext db = new DeliveryManagementDbContext();
        // GET: Products
        public ActionResult Index(int page=1)
        {
            return View(db.Products.OrderBy(x=>x.ProductId).ToPagedList(page, 5));
        }
        public ActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public ActionResult Edit(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public ActionResult Delete(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(Product product)
        {
            db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}