using Microsoft.Extensions.DependencyInjection;
using MoreleSeleniumFramework.Extensions;

namespace MoreleSeleniumWebsiteTests;

internal sealed class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddFramework();
	}
}