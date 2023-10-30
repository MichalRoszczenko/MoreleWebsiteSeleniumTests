using MoreleSeleniumFramework.Models;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages.Interfaces
{
    public interface IBasketPage
    {
        ReadOnlyCollection<IWebElement> ProductsInBasket { get; }

		List<ProductDetails> GetProductsFromBasket(ReadOnlyCollection<IWebElement> elementsInBasket);
		decimal GetSummaryBasketPrice();
	}
}