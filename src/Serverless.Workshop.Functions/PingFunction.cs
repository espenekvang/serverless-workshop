using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Serverless.Workshop.Functions
{
	public class PingFunction
	{
		public PingFunction()
		{
		}

		[FunctionName("ping")]
		public HttpResponseMessage Ping(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
		{
			return new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent("pong")
			};
		}

		private static IConfigurationRoot GetConfiguration(ExecutionContext context)
		{
			return new ConfigurationBuilder()
				.SetBasePath(context.FunctionAppDirectory)
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();
		}
	}
}
