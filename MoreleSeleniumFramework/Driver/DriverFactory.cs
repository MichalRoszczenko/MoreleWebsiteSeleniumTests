using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumFramework.Driver;

public sealed class DriverFactory : IDriverFactory, IDisposable
{
	public IWebDriver Driver { get; private set; }
	public DriverFactory()
	{
		ChromeDriver driver = new ChromeDriver();
		driver.Manage().Window.Size = new System.Drawing.Size(1280, 1024);
		driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
		driver.Navigate().GoToUrl("https://www.morele.net/");

		Driver = driver;
	}

	public WebDriverWait CreateWebDriverWait() => new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

	public void Dispose()
	{
		Driver.Quit();
	}
}
