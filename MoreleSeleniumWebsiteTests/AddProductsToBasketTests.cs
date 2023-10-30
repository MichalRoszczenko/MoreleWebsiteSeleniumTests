using FluentAssertions;
using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Pages.Interfaces;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MoreleSeleniumWebsiteTests
{
	public class AddProductsToBasketTests
	{
		private readonly IHomePage _homePage;
		private readonly IProductsInCategoryPage _productsInCategory;
		private readonly IBasketPage _basketPage;
		private readonly WebDriverWait _wait;
		private readonly Actions _actions;

		public AddProductsToBasketTests(IHomePage homePage, IDriverFactory driverFactory,
			IProductsInCategoryPage productsInCategory, IBasketPage basketPage)
        {
			_homePage = homePage;
			_productsInCategory = productsInCategory;
			_basketPage = basketPage;
			_wait = driverFactory.CreateWebDriverWait();
			_actions = driverFactory.CreateActions();
		}

        [Theory]
		[InlineData(3)]
		[InlineData(13)]
		[InlineData(5)]
		[InlineData(29)]
		public void AddProductToBasket_AddedProductShouldBeInBasket(int productNumber)
		{
			//act
			_wait.Until(d => _homePage.AcceptCookiesButtons.Displayed);
			_homePage.AcceptCookiesButtons.Click();
			_actions.MoveToElement(_homePage.ComputerComponentsMainCategory).Perform();
			_homePage.GraphicCardsSubCategory.Click();

			var products = _productsInCategory.Products;
			var product = _productsInCategory.GetProductInfo(products, productNumber);
			_productsInCategory.AddProductToBasket(products, productNumber);
			_wait.Until(d => _productsInCategory.WarrantyPopUp.Displayed);
			_productsInCategory.WarrantyWindowCloseButton.Click();

			_homePage.Basket.Click();
			var productsInBasket = _basketPage.GetProductsFromBasket(_basketPage.ProductsInBasket);

			//assert
			productsInBasket.Should().ContainEquivalentOf(product);
		}
	}
}