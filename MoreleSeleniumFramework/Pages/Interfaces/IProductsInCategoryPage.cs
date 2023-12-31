﻿using MoreleSeleniumFramework.Models;
using MoreleSeleniumFramework.Pages.Interfaces.Minor;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages.Interfaces
{
    public interface IProductsInCategoryPage : IProductFiltering, IProductSort
	{
		IWebElement WarrantyPopUp { get; }
		IWebElement WarrantyWindowCloseButton { get; }
		ReadOnlyCollection<IWebElement> ProductsProvidedByMorele { get; }
		ReadOnlyCollection<IWebElement> ProductsInCategory { get; }
		IWebElement LoadingPageCircle { get; }
		ReadOnlyCollection<IWebElement> ProductBrands { get; }
		void AddProductToBasket(ReadOnlyCollection<IWebElement> productsList, int productNumber);
		ProductDetails GetProductInfo(ReadOnlyCollection<IWebElement> productsList, int productNumber);
		List<ProductDetails> GetProductsInfo(ReadOnlyCollection<IWebElement> productsList);
        void GoToPage(int pageNumber);
		int GetNumberOfPages();
	}
}