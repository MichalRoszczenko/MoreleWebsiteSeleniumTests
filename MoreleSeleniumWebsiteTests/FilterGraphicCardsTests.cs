using FluentAssertions;
using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Pages.Interfaces;
using MoreleSeleniumFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumWebsiteTests;

public class FilterGraphicCardsTests
{
	private readonly IHomePage _homePage;
	private readonly IProductsInCategoryPage _productsInCategoryPage;
	private readonly WebDriverWait _wait;
	private readonly Actions _actions;

	public FilterGraphicCardsTests(IHomePage homePage, IDriverFactory driverFactory,
		IProductsInCategoryPage productsInCategoryPage)
    {
		_homePage = homePage;
		_productsInCategoryPage = productsInCategoryPage;
		_wait = driverFactory.CreateWebDriverWait();
		_actions = driverFactory.CreateActions();
	}

	[Theory]
	[InlineData("Producenci", "msi")]
	[InlineData("Producenci", "hp")]
	[InlineData("Producenci", "afox")]
	[InlineData("Producenci", "asus")]
	[InlineData("Producenci", "asrock")]
	[InlineData("Rodzaj pamięci RAM", "DDR2")]
	[InlineData("Rodzaj pamięci RAM", "DDR3")]
	[InlineData("Rodzaj pamięci RAM", "DDR4")]
	[InlineData("Rodzaj pamięci RAM", "GDDR3")]
	[InlineData("Rodzaj pamięci RAM", "GDDR5")]
	[InlineData("Rodzaj pamięci RAM", "hbm2e")]
	public void Filtering_using_selected_filter_returns_correct_number_of_filtered_cards(string filterCategory, string filtration)
	{
		//act
		PreliminarySetup();

		IWebElement filter = _productsInCategoryPage.SelectFiltrationInCategory(filterCategory, filtration);
		int filteredProductsCount = _productsInCategoryPage.NumberOfFilteredProducts(filter);
		_productsInCategoryPage.GetFilterShowMoreButton(filterCategory).Click();
		_productsInCategoryPage.SelectFilterCheckbox(filter).Click();

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
	public void Manufacturer_filter_result_shows_cards_with_correct_brand(string brandName)
	{
		//act
		PreliminarySetup();
		string filterCategory = "Producenci";

		IWebElement producerFilter = _productsInCategoryPage.SelectFiltrationInCategory(filterCategory, brandName);
		int filteredProductsCount = _productsInCategoryPage.NumberOfFilteredProducts(producerFilter);
		_productsInCategoryPage.GetFilterShowMoreButton(filterCategory).Click();
		_productsInCategoryPage.SelectFilterCheckbox(producerFilter).Click();

		_wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));

		List<string> brands = GetAllCardBrandsOnAvailableSites();

        //assert
        brands.Should().AllBe(brandName.ToLower());
    }

	[Theory]
	[InlineData("karty graficzne","Producenci" ,"pny")]
	public void Searched_cards_filtrated_by_brand_shows_cards_with_correct_brand(string searchKeyword,
		string filtrationCategory, string brandToFiltration)
	{
        //act
        PreliminarySetup();

        _homePage.SearchInputBar.SendKeys(searchKeyword.ToLower());
        _homePage.SearchInputBar.Submit();

        _productsInCategoryPage.GetFilterShowMoreButton(filtrationCategory).Click();
        IWebElement filter = _productsInCategoryPage.SelectFiltrationInCategory(filtrationCategory, brandToFiltration);
		_productsInCategoryPage.SelectFilterCheckbox(filter).Click();

		_wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));

        List<string> brands = GetAllCardBrandsOnAvailableSites();

        brands.Should().AllBe(brandToFiltration);
    }

	private void PreliminarySetup()
	{
		_homePage.CloseStartupPopups();
		_actions.MoveToElement(_homePage.ComputerComponentsMainCategory).Perform();
		_homePage.GraphicCardsSubCategory.Click();
	}

    private List<string> GetAllCardBrandsOnAvailableSites()
    {
        int numberOfPages = _productsInCategoryPage.GetNumberOfPages();
        List<string> brands = new List<string>();

        for (int i = 1; i <= numberOfPages; i++)
        {
            _wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));

            var brandElements = _productsInCategoryPage.ProductBrands;

            foreach (IWebElement element in brandElements)
            {
                string brand = element.GetAttribute("data-product-brand");
                brands.Add(brand.ToLower());
            }

            if (i != numberOfPages)
                _productsInCategoryPage.GoToPage(i + 1);
        }

        return brands;
    }
}