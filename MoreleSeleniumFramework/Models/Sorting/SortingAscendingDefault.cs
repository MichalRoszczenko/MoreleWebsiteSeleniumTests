using Newtonsoft.Json.Linq;
using System;

namespace MoreleSeleniumFramework.Models.Sorting;

public sealed class SortingAscendingDefault : SortingToggle
{
    public override string SortingOption { get; protected set; } = "default|asc";
}