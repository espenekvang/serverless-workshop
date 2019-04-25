using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Serverless.Workshop.Functions;

[assembly: WebJobsStartup(typeof(StartUp))]

namespace Serverless.Workshop.Functions
{
	public class StartUp : IWebJobsStartup
	{
		public void Configure(IWebJobsBuilder builder)
		{
			//Add your dependencies here if you need some
			//builder.Services.AddTransient<IMyDependency, MyDependency>();
		}
	}
}