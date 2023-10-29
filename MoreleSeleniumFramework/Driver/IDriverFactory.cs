using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumFramework.Driver
{
	public interface IDriverFactory
	{
		IWebDriver Driver { get; }
		WebDriverWait CreateWebDriverWait();
	}
}