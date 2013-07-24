namespace HarborApp.Controllers
{
    using System.Web.Mvc;
    using AppHarbor;
    using AttributeRouting.Web.Mvc;
    using System.Net;

    public class AppController : Controller
    {
        string client_id = "b6191a5e-e2fa-4648-a359-a165db58da43";
        string client_secret = "6142f6d9-bbe1-423a-9945-3293a19f200e";

        public ActionResult Auth(string code)
        {
            var authInfo = AppHarborClient.GetAuthInfo(client_id, client_secret, code);
            return Redirect("/app/token/?access_token=" + authInfo.AccessToken);
        }

        public ActionResult Token(string access_token)
        {
            return new HttpStatusCodeResult(200);
        }

        [Route("app/proxy/{method}/{url}", HttpVerbs.Post)]
        public ActionResult Proxy(string method, string url)
        {
            var req = WebRequest.Create(url);
            req.Method = method.ToUpperInvariant();
            return new HttpStatusCodeResult(200);
        }
    }
}
