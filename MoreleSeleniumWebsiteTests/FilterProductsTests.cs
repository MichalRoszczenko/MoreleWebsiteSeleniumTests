﻿using FluentAssertions;
using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Pages.Interfaces;
using MoreleSeleniumFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumWebsiteTests;

public class FilterProductsTests
{
	private readonly IHomePage _homePage;
	private readonly IProductsInCategoryPage _productsInCategoryPage;
	private readonly WebDriverWait _wait;
	private readonly Actions _actions;

	public FilterProductsTests(IHomePage homePage, IDriverFactory driverFactory,
		IProductsInCategoryPage productsInCategoryPage)
    {
		_homePage = homePage;
		_productsInCategoryPage = productsInCategoryPage;
		_wait = driverFactory.CreateWebDriverWait();
		_actions = driverFactory.CreateActions();
	}

	[Theory]
	[InlineData("msi")]
	[InlineData("hp")]
	[InlineData("afox")]
	[InlineData("asus")]
	[InlineData("asrock")]
	public void Manufacturer_filter_returns_correct_number_of_filtered_cards(string producerTag)
	{
		//act
		PreliminarySetup();

		IWebElement producerFilter = _productsInCategoryPage.SelectFiltratingByProducer(producerTag);
		int filteredProductsCount = _productsInCategoryPage.NumberOfFilteredProducts(producerFilter);
		_productsInCategoryPage.ProducersFilterShowMoreButton.Click();
		_productsInCategoryPage.ProducerFilterCheckbox(producerFilter).Click();

		int numberOfProductsOnLastPage = filteredProductsCount % 30;
		int pageNumber = (int)Math.Ceiling((decimal)filteredProductsCount / 30);

		if (filteredProductsCount > 30)
		{
			_wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));
			_productsInCategoryPage.GoToPage(pageNumber);
		}

		_wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));
		int productsShownOnPage = _productsInCategoryPage.ProductsInCategory.Count;

		//assert
		filteredProductsCount.Should().Be(productsShownOnPage+((pageNumber-1)*30));
	}

	[Theory]
	[InlineData("msi")]
	[InlineData("hp")]
	[InlineData("afox")]
	[InlineData("asus")]
	[InlineData("asrock")]
	public void Manufacturer_filter_result_shows_cards_with_correct_brand(string producerTag)
	{
		//act
		PreliminarySetup();

		IWebElement producerFilter = _productsInCategoryPage.SelectFiltratingByProducer(producerTag);
		int filteredProductsCount = _productsInCategoryPage.NumberOfFilteredProducts(producerFilter);
		_productsInCategoryPage.ProducersFilterShowMoreButton.Click();
		_productsInCategoryPage.ProducerFilterCheckbox(producerFilter).Click();

		_wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));

		int numberOfPages = _productsInCategoryPage.GetNumberOfPages();
		List<string> brands = new List<string>();

		for (int i = 1; i <= numberOfPages; i++)
		{
			_wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));
			var brandElements = _productsInCategoryPage.ProductBrand;

			foreach (var element in brandElements)
			{
				string brand = element.GetAttribute("data-product-brand");
				brands.Add(brand.ToLower());
			}

			if(i != numberOfPages)
			_productsInCategoryPage.GoToPage(i + 1);
		}

		//assert
		brands.Should().AllBe(producerTag.ToLower());
    }

	private void PreliminarySetup()
	{
		_wait.Until(d => _homePage.AcceptCookiesButtons.Displayed && _homePage.AcceptCookiesButtons.Enabled);
		_homePage.AcceptCookiesButtons.Click();
		_actions.MoveToElement(_homePage.ComputerComponentsMainCategory).Perform();
		_homePage.GraphicCardsSubCategory.Click();
	}
}