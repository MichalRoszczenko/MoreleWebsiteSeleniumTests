namespace MoreleSeleniumFramework.Models.Sorting;

public sealed class SortingDescendingByPopularity : SortingToggle
{
    public override string SortingOption { get; protected set; } = "sold|desc";
}
