
using ECommerce.Core.Entities.Search;
using ECommerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ECommerce.Infrastructure.Utility
{
    public static class LinqExtension
    {
        #region ## Propertires ##

        private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod(@"Contains", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null);

        #endregion ## Propertires ##

        public static IQueryable<T> ConvertToLinq<T>(IQueryable<T> query, SearchCriteria searchCriteria, out IQueryable<T> filteredQuery)
        {
            filteredQuery = query;

            if (searchCriteria == null)
            {
                return query;
            }

            query = ConvertFiltersToLinq(query, searchCriteria.FilterCriteria);
            filteredQuery = ConvertFiltersToLinq(query, searchCriteria.FilterCriteria);

            if (searchCriteria.SortCriteria != null)
            {
                query = ConvertSortToLinq(query, searchCriteria.SortCriteria);
            }

            if (searchCriteria.PageCriteria != null && searchCriteria.PageCriteria.AllowPaging && searchCriteria.PageCriteria.PageNumber.HasValue && searchCriteria.PageCriteria.PageSize.HasValue)
            {
                query = query.Skip((searchCriteria.PageCriteria.PageNumber.Value)
                                   * searchCriteria.PageCriteria.PageSize.Value).Take(searchCriteria.PageCriteria.PageSize.Value);
            }

            return query;
        }

        private static IQueryable<T> Filter<T>(this IQueryable<T> query, FilterCriteria filterCriteria)
        {
            var parameter = Expression.Parameter(typeof(T), string.Empty);

            var fieldExpression = ConstructExpressionsRecursively<T>(filterCriteria, parameter);
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(fieldExpression, parameter);

            query = query.Where(lambdaExpression);
            return query;
        }

        private static Expression ConstructExpressionsRecursively<T>(FilterCriteria filterCriteria, Expression parameter)
        {
            Expression nestedFieldExpression = null;
            if (filterCriteria.NestedFilterCriteria != null)
            {
                nestedFieldExpression = ConstructExpressionsRecursively<T>(filterCriteria.NestedFilterCriteria, parameter);
            }

            var fieldExpression = ConstructFieldExpression<T>(filterCriteria, parameter);

            if (nestedFieldExpression == null)
            {
                return fieldExpression;
            }

            Expression logicalOperatorExpression;
            switch (filterCriteria.LogicalOperator)
            {
                case LogicalOperator.And:
                    logicalOperatorExpression = Expression.And(fieldExpression, nestedFieldExpression);
                    break;

                case LogicalOperator.Or:
                    logicalOperatorExpression = Expression.Or(fieldExpression, nestedFieldExpression);
                    break;

                default:
                    throw new Exception("Invalid Logical Operator");
            }

            return logicalOperatorExpression;
        }

        private static Expression ConstructFieldExpression<T>(FilterCriteria filterCriteria, Expression parameter)
        {
            var fieldType = GetPropertyType<T>(filterCriteria.FilterName);
            var objValue = ChangeType(filterCriteria.FilterValue.ToString(), fieldType);

            Expression left = Expression.PropertyOrField(parameter, filterCriteria.FilterName);
            Expression right = Expression.Constant(objValue, fieldType);

            if (fieldType == typeof(DateTime?))
            {
                var leftValue = Expression.Property(left, "Value");
                var nullValue = Expression.Constant(null, typeof(DateTime?));
                left = Expression.Condition(
                    Expression.Equal(left, nullValue),
                    nullValue,
                    Expression.Convert(Expression.Property(leftValue, "Date"), typeof(DateTime?)));

                right = Expression.Constant(objValue);
                right = Expression.Convert(right, typeof(DateTime?));
            }
            else if (fieldType == typeof(DateTime))
            {
                left = Expression.Property(left, "Date");
            }

            Expression fieldExpression;
            switch (filterCriteria.FilterType)
            {
                case FilterType.Equals:
                    fieldExpression = Expression.Equal(left, right);
                    break;

                case FilterType.NotEquals:
                    fieldExpression = Expression.NotEqual(left, right);
                    break;

                case FilterType.GreaterThan:
                    fieldExpression = Expression.GreaterThan(left, right);
                    break;

                case FilterType.GreaterOrEquals:
                    fieldExpression = Expression.GreaterThanOrEqual(left, right);
                    break;

                case FilterType.LessOrEquals:
                    fieldExpression = Expression.LessThanOrEqual(left, right);
                    break;

                case FilterType.LessThan:
                    fieldExpression = Expression.LessThan(left, right);
                    break;

                case FilterType.Like:
                    fieldExpression = Expression.Call(left, StringContainsMethod, right);
                    break;

                default:
                    throw new Exception("Invalid Operation");
            }

            return fieldExpression;
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }

        private static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }

            //ToUse: var query = people.DistinctBy(p => p.Id);
            //ToUse multiple properties: var query = people.DistinctBy(p => new { p.Id, p.Name });
        }

        private static Type GetPropertyType<T>(string propertyName)
        {
            var props = propertyName.Split('.');
            var propertyType = typeof(T);

            foreach (var prop in props)
            {
                var pi = propertyType.GetProperty(prop);
                propertyType = pi.PropertyType;
            }

            return propertyType;
        }

        private static IQueryable<T> ConvertFiltersToLinq<T>(IQueryable<T> query, IList<FilterCriteria> filterList)
        {
            foreach (var item in filterList)
            {
                //query = Filter(query, item.FilterName, GetPropertyType<T>(item.FilterName), item.FilterType, item.FilterValue.ToString());
                query = Filter(query, item);
            }

            return query;
        }

        private static IQueryable<T> ConvertSortToLinq<T>(IQueryable<T> query, Sorting sortCriteria)
        {
            if (sortCriteria == null)
            {
                sortCriteria = new Sorting { Dir = SortingDirection.DESC, Field = "Id" };
            }

            return sortCriteria.Dir == SortingDirection.DESC ? OrderByDescending(query, sortCriteria.Field) : OrderBy(query, sortCriteria.Field);
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            var props = property.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            foreach (var prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                var pi = type.GetProperty(prop);
                if (pi == null)
                {
                    continue;
                }

                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        }

        private static object ChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException(nameof(conversionType));
            }

            if (!conversionType.IsGenericType || conversionType.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                return Convert.ChangeType(value, conversionType, System.Globalization.CultureInfo.InvariantCulture);
            }

            if (value == null)
            {
                return null;
            }

            var nullableConverter = new NullableConverter(conversionType);
            conversionType = nullableConverter.UnderlyingType;

            return Convert.ChangeType(value, conversionType, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}