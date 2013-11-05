using System.Web.Mvc;

namespace MVCForms.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to MVCForms, a MVC Front-End for MVC Dynamic Forms!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
