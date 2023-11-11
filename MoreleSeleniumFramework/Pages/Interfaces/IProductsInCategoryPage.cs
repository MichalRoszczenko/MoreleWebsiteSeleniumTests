using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces.Minor;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages.Interfaces
{
    public interface IProductsInCategoryPage : IProductFiltering
	{
		IWebElement WarrantyPopUp { get; }
		IWebElement WarrantyWindowCloseButton { get; }
		ReadOnlyCollection<IWebElement> ProductsProvidedByMorele { get; }
		ReadOnlyCollection<IWebElement> ProductsInCategory { get; }
		void AddProductToBasket(ReadOnlyCollection<IWebElement> productsList, int productNumber);
		ProductDetails GetProductInfo(ReadOnlyCollection<IWebElement> productsList, int productNumber);
		IWebElement SelectPageNumber(int pageNumber);
	}
}