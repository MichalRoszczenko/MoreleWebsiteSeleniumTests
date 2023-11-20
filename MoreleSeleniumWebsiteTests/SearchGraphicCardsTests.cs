using FluentAssertions;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace MoreleSeleniumWebsiteTests;

public class SearchGraphicCardsTests
{
	private readonly IHomePage _homePage;
	private readonly IProductsInCategoryPage _productsInCategory;

	public SearchGraphicCardsTests(IHomePage homePage,IProductsInCategoryPage productsInCategory)
	{
		_homePage = homePage;
		_productsInCategory = productsInCategory;
	}

	[Theory()]
	[InlineData("graficzna")]
	[InlineData("procesor")]
	[InlineData("płyta główna")]
	[InlineData("obudowa")]
	[InlineData("mysz")]
	public void Search_by_keyword_returns_cards_with_matching_names(string keyword)
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

	[Theory()]
	[InlineData("graficzna")]
	[InlineData("procesor")]
	[InlineData("płyta główna")]
	[InlineData("obudowa")]
	[InlineData("mysz")]
	public void Search_by_keyword_returns_cards_that_not_exceed_maximum_number_of_items_displayed(string keyword)
	{
		//act
		PreliminarySetup();

		_homePage.SearchInputBar.SendKeys(keyword.ToLower());
		_homePage.SearchInputBar.Submit();

		ReadOnlyCollection<IWebElement> products = _productsInCategory.ProductsProvidedByMorele;

		//assert
		products.Count.Should().BeLessThanOrEqualTo(30);
	}

	private void PreliminarySetup()
	{
		_homePage.CloseStartupPopups();
	}
}
