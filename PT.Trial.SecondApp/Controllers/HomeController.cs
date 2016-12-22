using System.Web.Mvc;

namespace PT.Trial.SecondApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("~/help");
        }
    }
}
