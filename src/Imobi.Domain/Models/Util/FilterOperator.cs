namespace Imobi.Domain.Models.Util
{
    public enum FilterOperator { Equals, Contains, GreaterThan, LessThan, Between, IsTrue, IsFalse }

    public class FilterItemDomain
    {
        public string PropertyName { get; set; } = string.Empty;
        public FilterOperator Operator { get; set; }
        public string? Value { get; set; }
        public string? ValueTo { get; set; }
    }

    public class SearchParametersDomain
    {
        public List<FilterItemDomain> Filters { get; set; } = new();
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
