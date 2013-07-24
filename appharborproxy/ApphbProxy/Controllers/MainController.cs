namespace AppHarborProxy.Controllers
{
    using System.IO;
    using System.Net;
    using System.Web.Mvc;
    using AttributeRouting.Web.Mvc;

    public class MainController : Controller
    {
        [Route("/{?proxyString}")]
        public ActionResult Index(string proxyString)
        {
            if (Request.IsAjaxRequest() && !string.IsNullOrWhiteSpace(proxyString))
            {
                var request = (HttpWebRequest)(HttpWebRequest.Create("https://appharbor.com/" + proxyString));
                request.Method = Request.HttpMethod;
                request.Headers.Add("Accept", "application/json");
                if (!string.IsNullOrWhiteSpace(Request.Headers["Authorization"]))
                {
                    request.Headers.Add("Authorization", Request.Headers["Authorization"]);
                }

                try
                {
                    var response = (HttpWebResponse)request.GetResponse();
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        Response.ContentType = response.ContentType;
                        using (var writer = new StreamWriter(response.GetResponseStream()))
                        {
                            writer.Write(reader.ReadToEnd());
                        }
                        return new HttpStatusCodeResult((int)response.StatusCode, response.StatusDescription);
                    }
                }
                catch (WebException ex)
                {
                    var response = (HttpWebResponse)ex.Response;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        Response.ContentType = response.ContentType;
                        using (var writer = new StreamWriter(response.GetResponseStream()))
                        {
                            writer.Write(reader.ReadToEnd());
                        }
                        return new HttpStatusCodeResult((int)response.StatusCode, response.StatusDescription);
                    }
                }
            }
            else
            {
                return View("Main");
            }
        }

    }
}
