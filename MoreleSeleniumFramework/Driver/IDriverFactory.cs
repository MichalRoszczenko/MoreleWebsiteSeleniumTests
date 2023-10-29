using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumFramework.Driver
{
	public interface IDriverFactory
	{
		ChromeDriver Driver { get; }
		WebDriverWait CreateWebDriverWait();
		Actions CreateActions();
	}
}