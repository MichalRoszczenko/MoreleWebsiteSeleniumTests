namespace MoreleSeleniumFramework.Models.Sorting;

public sealed class SortingAscendingByPrice : SortingToggle
{
    public override string SortingOption { get; protected set; } = "price|asc";
}
