using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using The_New_Paradise.Models;

namespace The_New_Paradise.Controllers
{
    public class Cart_ItemController : Controller
    {
        private ProjectHeavenEntities2 db = new ProjectHeavenEntities2();

        // GET: Cart_Item
        public ActionResult Index()
        {
            var cart_Item = db.Cart_Item.Include(c => c.Customer).Include(c => c.ServicesTable);
            return View(cart_Item.ToList());
        }

        // GET: Cart_Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            if (cart_Item == null)
            {
                return HttpNotFound();
            }
            return View(cart_Item);
        }

        // GET: Cart_Item/Create
        public ActionResult Create()
        {
            ViewBag.Cart_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name");
            ViewBag.Servive_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee");
            return View();
        }

        // POST: Cart_Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cart_Id,Servive_Id,PriceOfGivenService,QuanOfGivenServiceForGivenCust,TotalPriceOfGivenServiceForGivenCust")] Cart_Item cart_Item)
        {
            if (ModelState.IsValid)
            {
                db.Cart_Item.Add(cart_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cart_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", cart_Item.Cart_Id);
            ViewBag.Servive_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee", cart_Item.Servive_Id);
            return View(cart_Item);
        }

        // GET: Cart_Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            if (cart_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cart_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", cart_Item.Cart_Id);
            ViewBag.Servive_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee", cart_Item.Servive_Id);
            return View(cart_Item);
        }

        // POST: Cart_Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cart_Id,Servive_Id,PriceOfGivenService,QuanOfGivenServiceForGivenCust,TotalPriceOfGivenServiceForGivenCust")] Cart_Item cart_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cart_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", cart_Item.Cart_Id);
            ViewBag.Servive_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee", cart_Item.Servive_Id);
            return View(cart_Item);
        }

        // GET: Cart_Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            if (cart_Item == null)
            {
                return HttpNotFound();
            }
            return View(cart_Item);
        }

        // POST: Cart_Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart_Item cart_Item = db.Cart_Item.Find(id);
            db.Cart_Item.Remove(cart_Item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
