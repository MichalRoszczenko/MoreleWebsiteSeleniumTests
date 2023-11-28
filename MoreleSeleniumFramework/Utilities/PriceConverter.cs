using System.Globalization;

namespace MoreleSeleniumFramework.Utilities;

public static class PriceConverter
{
	public static decimal ParsePriceToDecimal(string price)
	{
        CultureInfo provider = new CultureInfo("pl-PL");
        NumberStyles style = NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol | NumberStyles.Number;

		return decimal.Parse(price.Replace(".", ","), style, provider);
	}
}
