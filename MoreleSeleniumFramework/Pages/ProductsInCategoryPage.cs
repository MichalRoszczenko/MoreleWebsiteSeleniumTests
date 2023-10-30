using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages;

public sealed class ProductsInCategoryPage : IProductsInCategoryPage
{
	private readonly ChromeDriver _driver;
	private readonly Actions _actions;

	public ProductsInCategoryPage(IDriverFactory driverFactory)
	{
		_driver = driverFactory.Driver;
		_actions = driverFactory.CreateActions();
	}

	public IWebElement WarrantyPopUp => _driver.FindElement(By.CssSelector("div[class*='accordion-text_desktop']"));
	public IWebElement WarrantyWindowCloseButton => _driver.FindElement(By.ClassName("md-header-close-btn"));
	public ReadOnlyCollection<IWebElement> ProductsProvidedByMorele => 
		_driver.FindElements(By.CssSelector("div[class='cat-product card']:not(div:has([data-controller='merchant-info']))"));

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
		string rawPrice = element.GetAttribute("data-product-price").Replace(".",",");
		decimal price = decimal.Parse(rawPrice);

		return new ProductDetails() { Name = name, Price = price };
	}
}
