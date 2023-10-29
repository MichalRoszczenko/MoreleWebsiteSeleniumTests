using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Pages.Interfaces;
using OpenQA.Selenium;

namespace MoreleSeleniumFramework.Pages;

public sealed class HomePage : IHomePage
{
	private readonly IWebDriver _driver;

	public HomePage(IDriverFactory driverFactory)
    {
        _driver = driverFactory.Driver;
	}

    public IWebElement Basket => _driver.FindElement(By.CssSelector("div>a[href*='koszyk']"));
	public IWebElement ComputerComponentsMainCategory => _driver.FindElement(By.CssSelector("li>a[href='/podzespoly-komputerowe/']"));
	public IWebElement GraphicCardsSubCategory => _driver.FindElement(By.CssSelector("li>a[href*='karty-graficzne']"));
	public IWebElement AcceptCookiesButtons => _driver.FindElement(By.CssSelector("div[id='cookie_box'] button"));
}
