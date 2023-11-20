using MoreleSeleniumFramework.Pages.Interfaces.Minor;
using OpenQA.Selenium;

namespace MoreleSeleniumFramework.Pages.Interfaces;

public interface IHomePage : ISearchElements
{
	IWebElement Basket { get; }
	IWebElement ComputerComponentsMainCategory { get; }
	IWebElement GraphicCardsSubCategory { get; }
	IWebElement AcceptCookiesButton { get; }
	IWebElement BFAdCloseButton { get; }
}