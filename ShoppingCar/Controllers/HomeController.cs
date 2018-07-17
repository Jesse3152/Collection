using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCar.Controllers
{
    public class HomeController : Controller
    {
        Models.ShoppingCarEntities db = new Models.ShoppingCarEntities();
        public ActionResult Index()
        {
            var result = (from s in db.Products select s).ToList();
            return View(result);
        }

        public ActionResult About()
        {
           
            return View();
        }

       
        public ActionResult Contact()
        {
           // ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}