using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_New_Paradise.Models;

namespace The_New_Paradise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Customer C = new Customer();
            C = (Customer)Session["CustomerEmail"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}