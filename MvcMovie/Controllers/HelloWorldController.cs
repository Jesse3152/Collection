using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult Index()
        {
            return View();
        }
        // GET: /HelloWorld/Welcome/ 
        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }
        /*
        public string Welcome(string name, int ID = 1)
        {
            //HttpUtility.HtmlEncode保護應用程序免受惡意輸入
            // return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);
            return HttpUtility.HtmlEncode("Hello " + name + ", ID: " + ID);
        }
         */
    }
}