using FluentAssertions;
using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Models.Sorting;
using MoreleSeleniumFramework.Pages.Interfaces;
using MoreleSeleniumFramework.Utilities;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumWebsiteTests;

public class SortGraphicCardsTests
{
    private readonly Actions _actions;
    private readonly WebDriverWait _wait;
    private readonly IHomePage _homePage;
    private readonly IProductsInCategoryPage _productsInCategoryPage;

    public SortGraphicCardsTests(IDriverFactory driverFactory, IHomePage homePage,
        IProductsInCategoryPage productsInCategoryPage)
    {
        _actions = driverFactory.CreateActions();
        _wait = driverFactory.CreateWebDriverWait();
        _homePage = homePage;
        _productsInCategoryPage = productsInCategoryPage;
    }

    [Fact]
    public void Sort_cards_by_ascending_price_show_cards_in_correct_order()
    {
        //act
        PreliminarySetup();
        _actions.MoveToElement(_homePage.ComputerComponentsMainCategory).Perform();
        _homePage.GraphicCardsSubCategory.Click();

        _productsInCategoryPage.SelectSortButton(new SortingAscendingByPrice());

        int numberOfPages = _productsInCategoryPage.GetNumberOfPages();
        List<ProductDetails> products = new List<ProductDetails>();

        for (int i = 1; i <= numberOfPages; i++)
        {
            _wait.Until(WaitFor.ElementInvisibility(_productsInCategoryPage.LoadingPageCircle));

            var productsOnPage = _productsInCategoryPage.GetProductsInfo(_productsInCategoryPage.ProductsInCategory);
            products.AddRange(productsOnPage);

            if(i != numberOfPages)
            _productsInCategoryPage.GoToPage(i + 1);

            //assert
            products.Should().BeInAscendingOrder(x => x.Price);
        }
    }

    private void PreliminarySetup()
    {
        _homePage.CloseStartupPopups();
    }
}
