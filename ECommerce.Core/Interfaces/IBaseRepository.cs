using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IBaseRepository<T>
         where T : class
    {
        void Add(T entity);
        IEnumerable<T> All(params string[] includes);

        IQueryable<T> AllQueryable(params string[] includes);

        PagedList<T> All(Paging paging, Sorting sorting = null);

        IEnumerable<T> Filter(Expression<Func<T, bool>> query, params string[] includes);

        IEnumerable<T> Filter(Expression<Func<T, bool>> query, bool track = true, params string[] includes);

        IQueryable<T> FilterQueryable(Expression<Func<T, bool>> query, params string[] includes);

        PagedList<T> Filter(Paging paging, Expression<Func<T, bool>> query, Sorting sorting = null);

        PagedList<T> Filter(Paging paging, Expression<Func<T, bool>> query, Sorting sorting = null, params string[] includes);

        PagedList<T> Filter(SearchCriteria searchCriteria, params string[] includes);

        PagedList<T> Filter(SearchCriteria searchCriteria, Expression<Func<T, bool>> query, params string[] includes);

        PagedList<T> FilterEnumerable(SearchCriteria searchCriteria, Expression<Func<T, bool>> query, params string[] includes);

        T FirstOrDefault(bool track = true);

        T FirstOrDefault(Expression<Func<T, bool>> query, bool track = true);

        T FirstOrDefault(Expression<Func<T, bool>> query, params string[] includes);

        T FirstOrDefault(Expression<Func<T, bool>> query, bool track = true, params string[] includes);

        int Count();

        int Count(Expression<Func<T, bool>> query);

        int Count(SearchCriteria searchCriteria, Expression<Func<T, bool>> query);

        bool Any();

        bool Any(Expression<Func<T, bool>> query);

        void Delete(T entity);

    }
}
