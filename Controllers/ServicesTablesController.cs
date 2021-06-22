using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using The_New_Paradise.Models;

namespace The_New_Paradise.Controllers
{
    public class ServicesTablesController : Controller
    {
        private ProjectHeavenEntities2 db = new ProjectHeavenEntities2();

        // GET: ServicesTables
       
        public ActionResult Index()
        {
          
            return View(db.ServicesTables.ToList());
        }

        // GET: ServicesTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesTable servicesTable = db.ServicesTables.Find(id);
            if (servicesTable == null)
            {
                return HttpNotFound();
            }
            return View(servicesTable);
        }

        // GET: ServicesTables/Create
        [Filters.AuthFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Service_ID,Service_Namee,Service_Price,Service_Time,Service_Description")] ServicesTable servicesTable, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(file.FileName);
                servicesTable.Service_Image = fileName;
                string uPath = Path.Combine(Server.MapPath("~/Upload"), fileName);
                file.SaveAs(uPath);
                db.ServicesTables.Add(servicesTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servicesTable);
        }

        // GET: ServicesTables/Edit/5
        [Filters.AuthFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesTable servicesTable = db.ServicesTables.Find(id);
            if (servicesTable == null)
            {
                return HttpNotFound();
            }
            return View(servicesTable);
        }

        // POST: ServicesTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public ActionResult Edit([Bind(Include = "Service_ID,Service_Namee,Service_Price,Service_Time,Service_Description")] ServicesTable servicesTable, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(file.FileName);
                servicesTable.Service_Image = fileName;
                string uPath = Path.Combine(Server.MapPath("~/Upload"), fileName);
                file.SaveAs(uPath);
                db.Entry(servicesTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servicesTable);
        }

        // GET: ServicesTables/Delete/5
        [Filters.AuthFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesTable servicesTable = db.ServicesTables.Find(id);
            if (servicesTable == null)
            {
                return HttpNotFound();
            }
            return View(servicesTable);
        }

        // POST: ServicesTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
     
        public ActionResult DeleteConfirmed(int id)
        {
            ServicesTable servicesTable = db.ServicesTables.Find(id);
            db.ServicesTables.Remove(servicesTable);
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
