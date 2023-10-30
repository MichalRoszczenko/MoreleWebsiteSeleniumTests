using FluentAssertions;
using MoreleSeleniumFramework.Driver;
using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

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
		public void AddProductToBasket_AddedGraphicCardShouldBeInBasket(int productNumber)
		{
			//act
			PreliminarySetup();

			var product = AddProductToBasket(_productsInCategory.ProductsProvidedByMorele, productNumber);

			_homePage.Basket.Click();
			var productsInBasket = _basketPage.GetProductsFromBasket(_basketPage.ProductsInBasket);

			//assert
			productsInBasket.Should().ContainEquivalentOf(product);
		}

		[Theory]
		[InlineData(25,4)]
		[InlineData(20,4)]
		[InlineData(13,4)]
		[InlineData(6,4)]
		public void AddProductsToBasket_AddedGraphicCardShouldBeInBasket(int startProduct, int productsToAdd)
		{
			//assert
			List<ProductDetails> addedProducts = new List<ProductDetails>();

			//act
			PreliminarySetup();

			var products = _productsInCategory.ProductsProvidedByMorele;

			ArgumentsRangeCheck(startProduct, productsToAdd, products.Count);

			for (int i = startProduct; i < startProduct+productsToAdd; i++)
			{
				var product = AddProductToBasket(products,i);

				addedProducts.Add(product);
			}

			_homePage.Basket.Click();
			var productsInBasket = _basketPage.GetProductsFromBasket(_basketPage.ProductsInBasket);

			//assert
			foreach (var item in addedProducts)
			{
				productsInBasket.Should().ContainEquivalentOf(item);
			}
		}

		private void PreliminarySetup()
		{
			_wait.Until(d => _homePage.AcceptCookiesButtons.Displayed);
			_homePage.AcceptCookiesButtons.Click();
			_actions.MoveToElement(_homePage.ComputerComponentsMainCategory).Perform();
			_homePage.GraphicCardsSubCategory.Click();
		}

		private ProductDetails AddProductToBasket(ReadOnlyCollection<IWebElement> products, int productNumber)
		{
			var product = _productsInCategory.GetProductInfo(products, productNumber);
			_productsInCategory.AddProductToBasket(products, productNumber);
			_wait.Until(d => _productsInCategory.WarrantyPopUp.Displayed);
			_productsInCategory.WarrantyWindowCloseButton.Click();

			return product;
		}

		private void ArgumentsRangeCheck(int startProduct, int numberOfProductsToAdd, int numberOfElementsOnPage)
		{
			if (startProduct < 0)
			{
				throw new ArgumentOutOfRangeException($"{nameof(startProduct)} must be equal to or greater than 0.");
			}
			if (startProduct + numberOfProductsToAdd > numberOfElementsOnPage)
			{
				throw new ArgumentOutOfRangeException($"The products in the category are in the defaulty range 0-{numberOfElementsOnPage-1}");
			}
			if (numberOfProductsToAdd <= 0 && numberOfProductsToAdd >= numberOfElementsOnPage)
			{
				throw new ArgumentOutOfRangeException($"{nameof(numberOfProductsToAdd)} must be less than {numberOfElementsOnPage} and greater than 0.");
			}
		}
	}
}