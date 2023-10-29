using OpenQA.Selenium.Chrome;

namespace MoreleSeleniumFramework.Driver
{
	public interface IDriverCreator
	{
		ChromeDriver Create();
	}
}