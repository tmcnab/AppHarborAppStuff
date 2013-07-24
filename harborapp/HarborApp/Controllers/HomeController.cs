namespace HarborApp.Controllers
{
    using System.Web.Mvc;
    using AttributeRouting.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("support/", HttpVerbs.Get)]
        public ActionResult Support()
        {
            return Redirect("http://harborapp.uservoice.com/");
        }

        [OutputCache(Duration = 3600)]
        [Route("gallery/", HttpVerbs.Get)]
        public ActionResult Gallery()
        {
            return PartialView();
        }
    }
}
