using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.AzureWebJobsTraceWriter;
using Microsoft.Azure.WebJobs.Host;

namespace Mslearn
{
    public static class SeriloggerTest
    {
        [FunctionName("SeriloggerTest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, TraceWriter log)
        {
            
            var levelSwitch = new LoggingLevelSwitch ();
            levelSwitch.MinimumLevel = LogEventLevel.Debug;
            var logger = new LoggerConfiguration ().Enrich.FromLogContext ()
                .MinimumLevel.ControlledBy (levelSwitch).WriteTo.TraceWriter(log).CreateLogger ();
            
            
            using (LogContext.PushProperty ("level", "warning")) {
                logger.Warning ("This is a warning level message");
            }

            string name = req.Query["name"];
            
            logger.Information ("query param 'name':" + name);
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                        : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
