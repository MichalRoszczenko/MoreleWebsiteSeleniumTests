using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumFramework.Driver;

public sealed class DriverFactory : IDriverFactory, IDisposable
{
	public ChromeDriver Driver { get; private set; }

	public DriverFactory(IDriverCreator driverCreator)
	{
		Driver = driverCreator.Create();
	}

	public WebDriverWait CreateWebDriverWait() => new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
	public Actions CreateActions() => new Actions(Driver);

	public void Dispose()
	{
		Driver.Quit();
	}
}
