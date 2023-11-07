using OpenQA.Selenium;

namespace MoreleSeleniumFramework.Pages.Interfaces.Minor;

public interface ISearchElements
{
	IWebElement SearchInputBar { get; }
	IWebElement SearchButton { get; }
}
