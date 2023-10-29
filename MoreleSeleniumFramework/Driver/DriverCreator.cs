using OpenQA.Selenium.Chrome;

namespace MoreleSeleniumFramework.Driver;

public sealed class DriverCreator : IDriverCreator
{
	public ChromeDriver Create()
	{
		ChromeDriver driver = new ChromeDriver();
		driver.Manage().Window.Size = new System.Drawing.Size(1280, 1024);
		driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
		driver.Navigate().GoToUrl("https://www.morele.net/");

		return driver;
	}
}
