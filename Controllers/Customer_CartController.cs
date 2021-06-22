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
    public class Customer_CartController : Controller
    {
        private ProjectHeavenEntities2 db = new ProjectHeavenEntities2();

        // GET: Customer_Cart
        public ActionResult Index()
        {
            var customer_Cart = db.Customer_Cart.Include(c => c.Customer);
            return View(customer_Cart.ToList());
        }

        // GET: Customer_Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Cart customer_Cart = db.Customer_Cart.Find(id);
            if (customer_Cart == null)
            {
                return HttpNotFound();
            }
            return View(customer_Cart);
        }

        // GET: Customer_Cart/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name");
        //    return View();
        //}

        // POST: Customer_Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Customer_Id,Total_Price,Total_Quantity")] Customer_Cart customer_Cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Customer_Cart.Add(customer_Cart);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", customer_Cart.Customer_Id);
        //    return View(customer_Cart);
        //}

        // GET: Customer_Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Cart customer_Cart = db.Customer_Cart.Find(id);
            if (customer_Cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", customer_Cart.Customer_Id);
            return View(customer_Cart);
        }

        // POST: Customer_Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Customer_Id,Total_Price,Total_Quantity")] Customer_Cart customer_Cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", customer_Cart.Customer_Id);
            return View(customer_Cart);
        }

        // GET: Customer_Cart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Cart customer_Cart = db.Customer_Cart.Find(id);
            if (customer_Cart == null)
            {
                return HttpNotFound();
            }
            return View(customer_Cart);
        }

        // POST: Customer_Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Cart customer_Cart = db.Customer_Cart.Find(id);
            db.Customer_Cart.Remove(customer_Cart);
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
