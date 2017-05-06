using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AwesomeApi1.Controllers
{

    public class ProbesController : ApiController
    {
        private string[] Ips = new[] {"10.0.0.4", "10.0.0.5", "10.0.0.6"};
        private int[] ports = new[] {8090, 8091, 8092};

        public async Task<string[]> Get()
        {
            var tasks = new List<Task<string>>();
            foreach (var ip in Ips)
            {
                foreach (var port in ports)
                {
                    tasks.Add(GetHealthResponse(ip,port));
                }
            }
            await Task.WhenAll(tasks.ToArray());

            var responses = new List<string>();
            foreach (var task in tasks)
            {
                responses.Add(task.Result);
            }
            return responses.ToArray();
        }

        public async Task<String> GetHealthResponse(string ip, int port)
        {
            var url = $"http://{ip}:{port}/health";
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(1);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return url + " IS UP!";
                }
            }
            catch (Exception ex)
            {
            }
            return url + " IS DOWN";
        }
    }
}
