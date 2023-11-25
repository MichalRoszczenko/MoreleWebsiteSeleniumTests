namespace MoreleSeleniumFramework.Models.Sorting;

public sealed class SortingDescendingByRate : SortingToggle
{
    public override string SortingOption { get; protected set; } = "rate|desc";
}
