using Microsoft.Extensions.DependencyInjection;
using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Pages;
using MoreleSeleniumFramework.Pages.Interfaces;

namespace MoreleSeleniumFramework.Extensions;

public static class ServiceCollectionExtension
{
	public static void AddFramework(this IServiceCollection services)
	{
		services.AddTransient<IDriverCreator, DriverCreator>();
		services.AddScoped<IDriverFactory, DriverFactory>();
		services.AddScoped<IHomePage, HomePage>();
	}
}