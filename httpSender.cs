using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace VNetTrainSendFunc
{
    public class httpSender
    {
        private readonly ILogger<httpSender> _logger;

        public httpSender(ILogger<httpSender> logger)
        {
            _logger = logger;
        }

        [Function("httpSender")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string finalResult = string.Empty;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://vnettrainfunc56fa2.azurewebsites.net/api/httpReceiver");
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(content);
                finalResult = content;
            }

            return new OkObjectResult(finalResult);
        }
    }
}
