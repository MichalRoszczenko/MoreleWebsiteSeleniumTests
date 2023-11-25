namespace MoreleSeleniumFramework.Models.Sorting;

public sealed class SortingDescendingByReview : SortingToggle
{
    public override string SortingOption { get; protected set; } = "review|desc";
}
