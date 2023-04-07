using System.Web.Mvc;

namespace FakturaMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }       
    }
}