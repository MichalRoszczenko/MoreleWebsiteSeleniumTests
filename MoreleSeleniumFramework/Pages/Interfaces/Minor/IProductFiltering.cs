using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace MoreleSeleniumFramework.Pages.Interfaces.Minor;

    public interface IProductFiltering
	{
		IWebElement GetFilterShowMoreButton(string filterCategory);
		ReadOnlyCollection<IWebElement> GetFiltersInCategory(string filtrationCategory);
		IWebElement SelectFiltrationInCategory(string filtrationCategory, string filtration);
		IWebElement SelectFilterCheckbox(IWebElement filter);
		int NumberOfFilteredProducts(IWebElement filter);
	}