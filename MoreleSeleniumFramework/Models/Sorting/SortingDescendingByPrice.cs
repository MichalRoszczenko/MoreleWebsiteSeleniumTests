namespace MoreleSeleniumFramework.Models.Sorting;

public sealed class SortingDescendingByPrice : SortingToggle
{
    public override string SortingOption { get; protected set; } = "price|desc";
}
