using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces;
using MoreleSeleniumFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages;

public sealed class BasketPage : IBasketPage
{
	private readonly ChromeDriver _driver;

	public BasketPage(IDriverFactory driverFactory)
	{
		_driver = driverFactory.Driver;
	}

	public ReadOnlyCollection<IWebElement> ProductsInBasket => _driver.FindElements(By.CssSelector(".basket-box-items>div"));

	public List<ProductDetails> GetProductsFromBasket(ReadOnlyCollection<IWebElement> elementsInBasket)
	{
		List<ProductDetails> products = new List<ProductDetails>();

		foreach (var element in elementsInBasket)
		{
			string name = element.GetAttribute("data-name");
			decimal price = PriceConverter.ParsePriceToDecimal(element.GetAttribute("data-price"));

			ProductDetails product = new ProductDetails() { Name = name, Price = price };

			products.Add(product);
		}

		return products;
	}

	public decimal GetSummaryBasketPrice()
	{
		IWebElement priceInElement = _driver.FindElement(By.CssSelector(".summary-box-price b"));
		decimal price = PriceConverter.ParsePriceToDecimal(priceInElement.Text);

		return price;
	}
}
