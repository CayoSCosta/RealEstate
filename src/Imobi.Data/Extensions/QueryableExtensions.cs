using System.Linq.Expressions;
using Imobi.Domain.Models.Util;

namespace Imobi.Infra.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, SearchParametersDomain parameters)
        {
            foreach (var filter in parameters.Filters)
            {
                // Ignora filtros vazios, exceto booleanos puros
                if (filter.Operator != FilterOperator.IsTrue &&
                    filter.Operator != FilterOperator.IsFalse &&
                    string.IsNullOrEmpty(filter.Value)) continue;

                query = query.Where(BuildPredicate<T>(filter));
            }
            return query;
        }

        private static Expression<Func<T, bool>> BuildPredicate<T>(FilterItemDomain filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression property = parameter;

            foreach (var member in filter.PropertyName.Split('.'))
                property = Expression.Property(property, member);

            var type = property.Type;
            Expression body;

            switch (filter.Operator)
            {
                case FilterOperator.Equals:
                    body = Expression.Equal(property, Expression.Constant(ConvertValue(filter.Value, type)));
                    break;

                case FilterOperator.Contains:
                    var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    body = Expression.Call(property, method!, Expression.Constant(filter.Value));
                    break;

                case FilterOperator.GreaterThan:
                    body = Expression.GreaterThan(property, Expression.Constant(ConvertValue(filter.Value, type)));
                    break;

                case FilterOperator.LessThan:
                    body = Expression.LessThan(property, Expression.Constant(ConvertValue(filter.Value, type)));
                    break;

                case FilterOperator.Between:
                    var valFrom = ConvertValue(filter.Value, type);
                    var valTo = ConvertValue(filter.ValueTo, type);

                    if (valFrom == null || valTo == null)
                        body = Expression.Constant(true);
                    else
                    {
                        var constantFrom = Expression.Constant(valFrom);
                        var constantTo = Expression.Constant(valTo);
                        var left = Expression.GreaterThanOrEqual(property, constantFrom);
                        var right = Expression.LessThanOrEqual(property, constantTo);
                        body = Expression.AndAlso(left, right);
                    }
                    break;

                case FilterOperator.IsTrue:
                    body = Expression.Equal(property, Expression.Constant(true));
                    break;

                case FilterOperator.IsFalse:
                    body = Expression.Equal(property, Expression.Constant(false));
                    break;

                default: throw new NotImplementedException();
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static object? ConvertValue(string? value, Type type)
        {
            if (string.IsNullOrEmpty(value)) return null;
            var targetType = Nullable.GetUnderlyingType(type) ?? type;

            if (targetType.IsEnum) return Enum.Parse(targetType, value);
            if (targetType == typeof(Guid)) return Guid.Parse(value);

            if (targetType == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out var dt))
                {
                    return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                }
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}