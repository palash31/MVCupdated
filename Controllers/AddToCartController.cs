using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_New_Paradise.Models;

namespace The_New_Paradise.Controllers
{
    public class AddToCartController : Controller
    {
        private ProjectHeavenEntities2 db = new ProjectHeavenEntities2();
        // GET: AddToCart
        [Filters.CustomerAuthFilter]
        public ActionResult Add(ServicesTable s)
        {
            if(Session["cart"]==null)
            {
                Cart_Item cartitem = new Cart_Item();
                cartitem.Cart_Id = (int)Session["CuurrentCustId"];
                cartitem.Servive_Id = s.Service_ID;
                cartitem.Price = s.Service_Price;
                db.Cart_Item.Add(cartitem);
                db.SaveChanges();
                Session["cart"] = cartitem;
                //ViewBag.cart = li.Count();
                ViewBag.cart = 1;
                Session["count"] = 1;
            }
            else
            {
                List<Cart_Item> li = (List<Cart_Item>)Session["cart"];
                Cart_Item cartitem = new Cart_Item();
                cartitem.Cart_Id = (int)Session["CuurrentCustId"];
                cartitem.Servive_Id = s.Service_ID;
                cartitem.Price = s.Service_Price;
                li.Add(cartitem);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                db.Cart_Item.Add(cartitem);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index", "ServicesTables");
        }
        [Filters.CustomerAuthFilter]
        public ActionResult MyCart()
        {
            if (Session["cart"] != null)
            {
                List<Cart_Item> list = (List<Cart_Item>)Session["cart"];
                List<ServicesTable> ls = new List<ServicesTable>();
                foreach(Cart_Item w in list)
                {
                    ServicesTable t = new ServicesTable();
                    t.Service_Namee = (from sn in db.ServicesTables where sn.Service_ID == w.Servive_Id select sn.Service_Namee).FirstOrDefault();
                    t.Service_Price = (from sn in db.ServicesTables where sn.Service_ID == w.Servive_Id select sn.Service_Price).FirstOrDefault(); ;
                    t.Service_Image = (from sn in db.ServicesTables where sn.Service_ID == w.Servive_Id select sn.Service_Image).FirstOrDefault(); ;
                    ls.Add(t);
                }
                ViewData.Model = ls;
                return View();
            }
            else
            {
                
                return RedirectToAction("Index","ServicesTables");
            }
        }
        public ActionResult Remove(ServicesTable sr)
        {
            List<Cart_Item> li = (List<Cart_Item>)Session["cart"];
            li.Remove((from a in li where a.Servive_Id == sr.Service_ID select a).First());
            Session["cart"] = li;
            db.Cart_Item.Remove((from b in db.Cart_Item where b.Servive_Id == sr.Service_ID && b.Cart_Id == (int)Session["CuurrentCustId"] select b).First());
            db.SaveChanges();
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("MyCart", "AddToCart");
        }
        public ActionResult OrderSuccess()
        {
            Customer cu = (Customer)Session["CustomerEmail"];
            List<Cart_Item> crt = (List<Cart_Item>)Session["cart"];

            List<ServicesTable> ls2 = new List<ServicesTable>();
            foreach (Cart_Item w in crt)
            {
                ServicesTable t = new ServicesTable();
                t.Service_Namee = (from sn in db.ServicesTables where sn.Service_ID == w.Servive_Id select sn.Service_Namee).FirstOrDefault();
                t.Service_Price = (from sn in db.ServicesTables where sn.Service_ID == w.Servive_Id select sn.Service_Price).FirstOrDefault(); ;
                t.Service_Image = (from sn in db.ServicesTables where sn.Service_ID == w.Servive_Id select sn.Service_Image).FirstOrDefault(); ;
                ls2.Add(t);
            }

            ViewData["Customers"] = cu;
            ViewData["Services"] = ls2;
            return View();
        }

    }
}