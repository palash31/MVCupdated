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
    public class CustomersController : Controller
    {
        private ProjectHeavenEntities2 db = new ProjectHeavenEntities2();

        // GET: Customers

        [Filters.AuthFilter]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_Name,Customer_Gender,Customer_Phone,Customer_Address,Customer_Email,Customer_Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //ViewBag["CurrentCustomerId"] = customer.Customer_ID;
                db.Customers.Add(customer);
                //Customer_Cart cust_cart = new Customer_Cart(){Customer_Id=customer.Customer_ID};
                //db.Customer_Cart.Add(cust_cart);
                Session["cart"] = null;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_ID,Customer_Name,Customer_Gender,Customer_Phone,Customer_Address,Customer_Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult CustSignIn()
        {
            return View(new Models.Customer());
        }


        [HttpPost]
        public ActionResult CustSignIn(Models.Customer obj)
        {
            if (!ModelState.IsValid)
                return View(obj);
            else
            {
                using (var context = new ProjectHeavenEntities2())
                {
                    Models.Customer cust = context.Customers.Where(u => u.Customer_Email == obj.Customer_Email && u.Customer_Password == obj.Customer_Password).FirstOrDefault();
                    if (cust != null)
                    {
                        
                        Session["CustomerEmail"] = cust;
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        
                        ModelState.AddModelError("", "Invalid User Email or Password");
                        return View(obj);
                    }
                }
            }
        }
        public ActionResult CustSignOut()
        {
            Session["CustomerEmail"] = null;
            return RedirectToAction("Index", "Home");
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
