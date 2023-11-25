using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Models.Sorting;
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
	public ReadOnlyCollection<IWebElement> ProductBrands =>
		_driver.FindElements(By.CssSelector("div[data-product-brand]"));
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
    public List<ProductDetails> GetProductsInfo(ReadOnlyCollection<IWebElement> productsList)
    {
        List<ProductDetails> products = new List<ProductDetails>();

        foreach (IWebElement element in productsList)
        {
            string name = element.GetAttribute("data-product-name");
            decimal price = PriceConverter.ParsePriceToDecimal(element.GetAttribute("data-product-price"));
            products.Add(new ProductDetails() { Name = name, Price = price });
        }

        return products;
    }
    public IWebElement SelectFiltrationInCategory(string filtrationCategory, string filtration)
	{
		var filters = GetFiltersInCategory(filtrationCategory);

		IWebElement filter = filters
			.First(x => x.GetAttribute("data-name").ToLower() == filtration.ToLower());

		return filter;
	}
	public ReadOnlyCollection<IWebElement> GetFiltersInCategory(string filtrationCategory) =>
		_driver.FindElements(By.CssSelector($"div[data-fhead-name='{filtrationCategory}'] div[data-name]"));
	public IWebElement SelectFilterCheckbox(IWebElement producerFilter) => producerFilter.FindElement(By.CssSelector("label"));
	public int NumberOfFilteredProducts(IWebElement filter) => int.Parse(filter.GetAttribute("data-count"));
	public int GetNumberOfPages()
	{
		string attribute = _driver.FindElement(By.CssSelector("ul[data-count]")).GetAttribute("data-count");
		int numberOfPages = int.Parse(attribute);

		return numberOfPages;
	}
	public IWebElement GetFilterShowMoreButton(string filterCategory) =>
	_driver.FindElement(By.CssSelector($"div[data-fhead-name='{filterCategory}'] span[data-trans-open]"));

	public void SelectSortButton(SortingToggle value)
	{
		_driver.FindElement(By.CssSelector("select[name=category_sort]+button")).Click();
		_driver.FindElement(By.CssSelector($"li[data-dropdown-value='{value.SortingOption}']")).Click();
	}
}