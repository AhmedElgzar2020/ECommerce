
using ECommerce.Core.Entities.Search;
using ECommerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Utility
{
    public static class QueryableExtensions
    {
        private static int _pageSize;
        private static int _maxPageSize;
        //private static IConfiguration configuration;

        private static int PageSize
        {
            get
            {
                if (_pageSize == 0)
                {
                    //_pageSize = configuration?.GetValue<int>(AppSettingKeyConstants.PageSize) ?? 0;
                    _pageSize = 10;
                }

                return _pageSize != 0 ? _pageSize : 10;
            }
        }

        private static int MaxPageSize
        {
            get
            {
                if (_maxPageSize == 0)
                {
                    //_maxPageSize = configuration?.GetValue<int>(AppSettingKeyConstants.MaxPageSize) ?? 0;
                    _maxPageSize = 100;
                }

                return _maxPageSize != 0 ? _maxPageSize : 100;
            }
        }

        public static IQueryable<TEntity> SelectPage<TEntity>(this IQueryable<TEntity> query, Paging paging)
        {
            if (paging == null || !(paging.PageNumber >= 0))
            {
                return query;
            }

            var pageSize = paging.PageSize.HasValue && paging.PageSize.Value > 0 && paging.PageSize.Value <= MaxPageSize ? paging.PageSize.Value : PageSize;

            query = query.Skip(paging.PageNumber.Value * pageSize).Take(pageSize);

            return query;
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, Sorting sorting)
        {
            if (string.IsNullOrWhiteSpace(sorting?.Field))
            {
                return query;
            }

            var propertyNames = sorting.Field.Split(".");

            var pe = Expression.Parameter(typeof(TEntity), string.Empty);
            var property = propertyNames.Aggregate<string, Expression>(pe, Expression.PropertyOrField);
            var lambda = Expression.Lambda(property, pe);
            var orderByDir = sorting.Dir == SortingDirection.ASC ? "OrderBy" : "OrderByDescending";

            var call = Expression.Call(typeof(Queryable), orderByDir, new[] { typeof(TEntity), property.Type }, query.Expression, Expression.Quote(lambda));

            return (IOrderedQueryable<TEntity>)query.Provider.CreateQuery<TEntity>(call);
        }
    }
}
