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
        // GET: AddToCart
        [Filters.CustomerAuthFilter]
        public ActionResult Add(ServicesTable s)
        {
            if(Session["cart"]==null)
            {
                List<ServicesTable> li = new List<ServicesTable>();
                li.Add(s);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = 1;
                
            }
            else
            {
                List<ServicesTable> li = (List<ServicesTable>)Session["cart"];
                li.Add(s);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            
            return RedirectToAction("Index", "ServicesTables");
        }
        [Filters.CustomerAuthFilter]
        public ActionResult MyCart()
        {
            if (Session["cart"] != null)
            {
                List<ServicesTable> list = (List<ServicesTable>)Session["cart"];
                ViewData.Model = list;
                return View();
            }
            else
            {
                
                return RedirectToAction("Index","ServicesTables");
            }
        }
        public ActionResult Remove(ServicesTable sr)
        {
            List<ServicesTable> li = (List<ServicesTable>)Session["cart"];
            li.RemoveAll(x => x.Service_ID == sr.Service_ID);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("MyCart", "AddToCart");
        }
        public ActionResult OrderSuccess()
        {
            Customer cu = (Customer)Session["CustomerEmail"];
            ViewData.Model = cu;
            return View();
        }

    }
}