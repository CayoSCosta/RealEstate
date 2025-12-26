namespace Imobi.MVC.ViewModels.Util
{
    public class QueryParameters
    {
        public List<FilterItem> Filters { get; set; } = new();
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class FilterItem
    {
        public string PropertyName { get; set; } = string.Empty;
        public FilterOperator Operator { get; set; }
        public string? Value { get; set; }
        public string? ValueTo { get; set; }
    }

    public enum FilterOperator { Equals, Contains, GreaterThan, LessThan, Between, IsTrue, IsFalse }
}
