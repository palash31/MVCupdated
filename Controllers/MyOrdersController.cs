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

        public ActionResult Add()
        {
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "ServicesTables");
            }
            else
            {
                List<Cart_Item> li = (List<Cart_Item>)Session["cart"];
                foreach (Cart_Item cart in li)
                {
                    MyOrder myorder = new MyOrder();
                    myorder.Order_Id = (int)Session["CuurrentCustId"];
                    myorder.Service_Id = cart.Servive_Id;
                    db.MyOrders.Add(myorder);
                    db.SaveChanges();
                }
                return RedirectToAction("OrderSuccess", "AddToCart");
            }

                //Session["cart"] = li;
                //ViewBag.cart = li.Count();
                //Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                //db.Cart_Item.Add(cartitem);
                //db.SaveChanges();
                //foreach (ServicesTable serv in servics)
                //{
                //    MyOrder orderitem = new MyOrder();
                //    orderitem.Order_Id = (int)Session["CuurrentCustId"];
                //    orderitem.Service_Id = serv.Service_ID;
                //    db.MyOrders.Add(orderitem);
                //    db.SaveChanges();
                //}
            
            //List<Cart_Item> lst = new List<Cart_Item>();
            //    lst = (List<Cart_Item>)Session["cart"];
            //    orderitem.Service_Id = s.Service_ID;
            //    cartitem.Price = s.Service_Price;
            //        db.Cart_Item.Add(cartitem);
            //        db.SaveChanges();
            //        Session["cart"] = cartitem;
            //        //ViewBag.cart = li.Count();
            //        ViewBag.cart = 1;
            //        Session["count"] = 1;
            //    }
            //    else
            //    {
            //        List<Cart_Item> li = (List<Cart_Item>)Session["cart"];
            //        Cart_Item cartitem = new Cart_Item();
            //        cartitem.Cart_Id = (int)Session["CuurrentCustId"];
            //        cartitem.Servive_Id = s.Service_ID;
            //        cartitem.Price = s.Service_Price;
            //        li.Add(cartitem);
            //        Session["cart"] = li;
            //        ViewBag.cart = li.Count();
            //        Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            //        db.Cart_Item.Add(cartitem);
            //        db.SaveChanges();
            //    }

            //    return RedirectToAction("Index", "ServicesTables");
            //}
        }
        // GET: MyOrders
        [Filters.CustomerAuthFilter]
        public ActionResult Index()
        {
            List<ServicesTable> Ls = new List<ServicesTable>();
            List<MyOrder> Lst = new List<MyOrder>();
            Lst = (from myO in db.MyOrders.AsEnumerable() where myO.Order_Id == (int)Session["CuurrentCustId"] select myO).ToList();
            foreach (MyOrder myorder in Lst)
            {
                ServicesTable t = new ServicesTable();
                t.Service_Namee = (from sn in db.ServicesTables where sn.Service_ID == myorder.Service_Id select sn.Service_Namee).FirstOrDefault();
                t.Service_Price = (from sn in db.ServicesTables where sn.Service_ID == myorder.Service_Id select sn.Service_Price).FirstOrDefault(); ;
                t.Service_Image = (from sn in db.ServicesTables where sn.Service_ID == myorder.Service_Id select sn.Service_Image).FirstOrDefault(); ;
                Ls.Add(t);
            }
            ViewData.Model = Ls;
            return View();
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
