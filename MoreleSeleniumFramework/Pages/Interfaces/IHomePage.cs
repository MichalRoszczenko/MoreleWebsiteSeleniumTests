using OpenQA.Selenium;

namespace MoreleSeleniumFramework.Pages.Interfaces;

public interface IHomePage
{
	IWebElement Basket { get; }
	IWebElement ComputerComponentsMainCategory { get; }
	IWebElement GraphicCardsSubCategory { get; }
	IWebElement AcceptCookiesButtons { get; }
}