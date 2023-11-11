using OpenQA.Selenium;

namespace MoreleSeleniumFramework.Pages.Interfaces.Minor;

    public interface IProductFiltering
	{
		IWebElement ProducersFilterShowMoreButton { get; }
		IWebElement SelectFiltratingByProducer(string producerName);
		IWebElement ProducerFilterCheckbox(IWebElement producerFilter);
		int NumberOfFilteredProducts(IWebElement filter);
	}