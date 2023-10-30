using OpenQA.Selenium;

namespace MoreleSeleniumFramework.Utilities;

public static class PriceConverter
{
	public static decimal ParsePriceToDecimal(IWebElement priceElement)
	{
		string rawprice = priceElement.Text;
		string price = rawprice.Replace("zł", "");
		return decimal.Parse(price.Replace(".", ","));
	}	
	public static decimal ParsePriceToDecimal(string rawprice)
	{
		string price = rawprice.Replace("zł", "");
		return decimal.Parse(price.Replace(".", ","));
	}
}
