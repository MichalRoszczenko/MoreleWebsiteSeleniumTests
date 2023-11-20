using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Pages.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumFramework.Pages;

public sealed class HomePage : IHomePage
{
	private readonly IWebDriver _driver;
	private readonly WebDriverWait _wait;

	public HomePage(IDriverFactory driverFactory)
    {
        _driver = driverFactory.Driver;
		_wait = driverFactory.CreateWebDriverWait();
	}

    public IWebElement Basket => _driver.FindElement(By.CssSelector("div>a[href*='koszyk']"));
	public IWebElement ComputerComponentsMainCategory => _driver.FindElement(By.CssSelector("li>a[href='/podzespoly-komputerowe/']"));
	public IWebElement GraphicCardsSubCategory => _driver.FindElement(By.CssSelector("li>a[href*='karty-graficzne']"));
	public IWebElement AcceptCookiesButton => _driver.FindElement(By.CssSelector("div[id='cookie_box'] button"));
	public IWebElement BFAdCloseButton => _driver.FindElement(By.CssSelector("[title='zamknij']"));

	public IWebElement SearchInputBar => _driver.FindElement(By.CssSelector("header input[type='search']"));
	public IWebElement SearchButton => _driver.FindElement(By.CssSelector("header button[type='submit']"));

	public void CloseStartupPopups()
	{
		BFAdCloseButton.Click();
		_wait.Until(d => AcceptCookiesButton.Displayed && AcceptCookiesButton.Enabled);
		AcceptCookiesButton.Click();
	}
}
