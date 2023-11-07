using FluentAssertions;
using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace MoreleSeleniumWebsiteTests;

public class SearchProductsTests
{
	private readonly IHomePage _homePage;
	private readonly IProductsInCategoryPage _productsInCategory;
	private readonly WebDriverWait _wait;

	public SearchProductsTests(IDriverFactory driverFactory, IHomePage homePage,
			IProductsInCategoryPage productsInCategory)
	{
		_homePage = homePage;
		_productsInCategory = productsInCategory;
		_wait = driverFactory.CreateWebDriverWait();
	}

	[Theory()]
	[InlineData("graficzna")]
	[InlineData("procesor")]
	[InlineData("filtr")]
	[InlineData("płyta główna")]
	[InlineData("obudowa")]
	[InlineData("mysz")]
	public void SearchForProductsByKeyword_SearchResultsIncludeProductNamesWithMatchingKeyword(string keyword)
	{
		//act
		PreliminarySetup();

		_homePage.SearchInputBar.SendKeys(keyword.ToLower());
		_homePage.SearchInputBar.Submit();

		ReadOnlyCollection<IWebElement> products = _productsInCategory.ProductsInCategory;
		List<ProductDetails> productDetails = new List<ProductDetails>();

		for (int i = 0; i < products.Count; i++)
		{
			var product = _productsInCategory.GetProductInfo(products, i);

			productDetails.Add(product);
		}

		//assert
		foreach (var product in productDetails)
		{
			var name = product.Name.ToLower();
			name.Should().Contain(keyword.ToLower());
		}
	}

	private void PreliminarySetup()
	{
		_wait.Until(d => _homePage.AcceptCookiesButtons.Displayed && _homePage.AcceptCookiesButtons.Enabled);
		_homePage.AcceptCookiesButtons.Click();
	}
}
