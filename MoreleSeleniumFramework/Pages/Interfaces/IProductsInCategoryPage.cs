using MoreleSeleniumFramework.Models;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages.Interfaces
{
    public interface IProductsInCategoryPage
    {
		IWebElement WarrantyPopUp { get; }
		IWebElement WarrantyWindowCloseButton { get; }
		ReadOnlyCollection<IWebElement> ProductsProvidedByMorele { get; }
		void AddProductToBasket(ReadOnlyCollection<IWebElement> productsList, int productNumber);
		ProductDetails GetProductInfo(ReadOnlyCollection<IWebElement> productsList, int productNumber);
	}
}