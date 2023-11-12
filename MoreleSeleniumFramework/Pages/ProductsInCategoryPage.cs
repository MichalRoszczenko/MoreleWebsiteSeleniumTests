using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces;
using MoreleSeleniumFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages;

public sealed class ProductsInCategoryPage : IProductsInCategoryPage
{
	private readonly ChromeDriver _driver;
	private readonly Actions _actions;
	private readonly WebDriverWait _wait;

	public ProductsInCategoryPage(IDriverFactory driverFactory)
	{
		_driver = driverFactory.Driver;
		_actions = driverFactory.CreateActions();
		_wait = driverFactory.CreateWebDriverWait();
	}

	public IWebElement WarrantyPopUp => _driver.FindElement(By.CssSelector("div[class*='accordion-text_desktop']"));
	public IWebElement WarrantyWindowCloseButton => _driver.FindElement(By.ClassName("md-header-close-btn"));
	public ReadOnlyCollection<IWebElement> ProductsProvidedByMorele => 
		_driver.FindElements(By.CssSelector("div[class='cat-product card']:not(div:has([data-controller='merchant-info']))"));	
	public ReadOnlyCollection<IWebElement> ProductsInCategory => 
		_driver.FindElements(By.CssSelector("div[class='cat-product card']"));
	public IWebElement LoadingPageCircle => _driver.FindElement(By.CssSelector("i[class='circle-loading']"));
	public ReadOnlyCollection<IWebElement> ProducersFilter =>
		_driver.FindElements(By.CssSelector("div[data-fhead-name='Producenci'] div[data-name]"));
	public IWebElement ProducersFilterShowMoreButton =>
		_driver.FindElement(By.CssSelector("div[data-fhead-name='Producenci'] span[data-trans-open]"));

	public void GoToPage(int pageNumber)
	{
		switch (pageNumber)
		{
			case <= 0:
				throw new ArgumentOutOfRangeException();

			case <= 3:
				_driver.FindElement(By
					.CssSelector($".pagination.dynamic a[data-page='{pageNumber}']")).Click();
				break;

			case <= 5:
				_driver.FindElement(By
					.CssSelector($".pagination.dynamic a[data-page='{pageNumber - 1}']")).Click();
				_wait.Until(WaitFor.ElementInvisibility(LoadingPageCircle));
				_driver.FindElement(By.CssSelector($"li:has([rel='next'])")).Click();
				break;

			default:
				var chooseSite = _driver.FindElement(By.CssSelector($"li[class*='choose-site']>input"));
				chooseSite.Click();
				chooseSite.SendKeys(pageNumber.ToString());
				_driver.FindElement(By.CssSelector($"li:has([rel='next'])")).Click();
				break;
		}
	}
	public void AddProductToBasket(ReadOnlyCollection<IWebElement> productsList, int productNumber)
	{
		IWebElement element = productsList[productNumber].FindElement(By.ClassName("cat-product-buttons"));
		_actions.MoveToElement(element).Perform();
		element.Click();
	}
	public ProductDetails GetProductInfo(ReadOnlyCollection<IWebElement> productsList, int productNumber)
	{
		IWebElement element = productsList[productNumber];

		string name = element.GetAttribute("data-product-name");
		decimal price = PriceConverter.ParsePriceToDecimal(element.GetAttribute("data-product-price"));

		return new ProductDetails() { Name = name, Price = price };
	}

	public IWebElement SelectFiltratingByProducer(string producerName)
	{
		var producers = ProducersFilter;

		IWebElement producer = producers
			.First(x => x.GetAttribute("data-name").ToLower() == producerName.ToLower());

		return producer;
	}
	public IWebElement ProducerFilterCheckbox(IWebElement producerFilter) => producerFilter.FindElement(By.CssSelector("label"));
	public int NumberOfFilteredProducts(IWebElement filter) => int.Parse(filter.GetAttribute("data-count"));
}
