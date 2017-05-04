using System;
using System.Net;
using System.Web.Http;

namespace AwesomeApi1.Controllers
{
    public class HealthController : ApiController
    {
        public static bool Healthy { get; set; } = true;

        public string Get()
        {
            if (Healthy)
            {
                return nameof(AwesomeApi1) + " UP " + DateTime.Now.ToShortTimeString();
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
