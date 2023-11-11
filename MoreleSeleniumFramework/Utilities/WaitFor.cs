using OpenQA.Selenium;

namespace MoreleSeleniumFramework.Utilities;

public static class WaitFor
{
	public static Func<IWebDriver,bool> ElementInvisibility(IWebElement webElement)
	{
		return delegate (IWebDriver driver)
		{
			try
			{
				return !webElement.Displayed;
			}
			catch (NoSuchElementException)
			{
				return true;
			}
			catch (StaleElementReferenceException)
			{
				return true;
			}
		};
	}
}
