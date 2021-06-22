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
    public class MyOrdersController : Controller
    {
        private ProjectHeavenEntities2 db = new ProjectHeavenEntities2();

        // GET: MyOrders
        public ActionResult Index()
        {
            var myOrders = db.MyOrders.Include(m => m.Customer).Include(m => m.ServicesTable);
            return View(myOrders.ToList());
        }

        // GET: MyOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyOrder myOrder = db.MyOrders.Find(id);
            if (myOrder == null)
            {
                return HttpNotFound();
            }
            return View(myOrder);
        }

        // GET: MyOrders/Create
        public ActionResult Create()
        {
            ViewBag.Order_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name");
            ViewBag.Service_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee");
            return View();
        }

        // POST: MyOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order_Id,Service_Id")] MyOrder myOrder)
        {
            if (ModelState.IsValid)
            {
                db.MyOrders.Add(myOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Order_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", myOrder.Order_Id);
            ViewBag.Service_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee", myOrder.Service_Id);
            return View(myOrder);
        }

        // GET: MyOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyOrder myOrder = db.MyOrders.Find(id);
            if (myOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", myOrder.Order_Id);
            ViewBag.Service_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee", myOrder.Service_Id);
            return View(myOrder);
        }

        // POST: MyOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order_Id,Service_Id")] MyOrder myOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Order_Id = new SelectList(db.Customers, "Customer_ID", "Customer_Name", myOrder.Order_Id);
            ViewBag.Service_Id = new SelectList(db.ServicesTables, "Service_ID", "Service_Namee", myOrder.Service_Id);
            return View(myOrder);
        }

        // GET: MyOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyOrder myOrder = db.MyOrders.Find(id);
            if (myOrder == null)
            {
                return HttpNotFound();
            }
            return View(myOrder);
        }

        // POST: MyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyOrder myOrder = db.MyOrders.Find(id);
            db.MyOrders.Remove(myOrder);
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
