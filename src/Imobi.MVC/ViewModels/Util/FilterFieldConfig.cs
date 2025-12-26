namespace Imobi.MVC.ViewModels.Util
{
    public class FilterFieldConfig
    {
        public string PropertyName { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public Type Type { get; set; } = typeof(string);
        public FilterOperator Operator { get; set; }
        public int Index { get; set; }
        public string? CssClass { get; set; }
    }
}
