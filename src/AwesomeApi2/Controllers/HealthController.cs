using System;
using System.Net;
using System.Web.Http;

namespace AwesomeApi2.Controllers
{
    public class HealthController : ApiController
    {
        public static bool Healthy { get; set; } = true;

        public string Get()
        {
            if (Healthy)
            {
                return nameof(AwesomeApi2) + " UP " + DateTime.Now.ToShortTimeString();
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
