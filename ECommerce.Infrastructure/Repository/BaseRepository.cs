using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Search;
using ECommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Utility;

namespace ECommerce.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        public BaseRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        protected DbContext Context { get; set; }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            //Context.SaveChanges();
        }
        public IEnumerable<T> All(params string[] includes)
        {
            var collection = Context.Set<T>().AsQueryable();

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    collection = collection.Include(include);
                }
            }

            return collection.AsEnumerable();
        }

        public IQueryable<T> AllQueryable(params string[] includes)
        {
            var collection = Context.Set<T>().AsQueryable();

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    collection = collection.Include(include);
                }
            }

            return collection;
        }

        public PagedList<T> All(Paging paging, Sorting sorting = null)
        {
            var collection = Context.Set<T>()
                .OrderBy(sorting)
                .SelectPage(paging);

            return new PagedList<T>
            {
                Collection = collection.ToList(),
                TotalCount = Count(),
            };
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> query, params string[] includes)
        {
            var collection = Context.Set<T>().Where(query);

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    collection = collection.Include(include);
                }
            }

            return collection.AsEnumerable();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> query, bool track = true, params string[] includes)
        {
            var collection = track ? Context.Set<T>().Where(query) : Context.Set<T>().AsNoTracking().Where(query);

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    collection = collection.Include(include);
                }
            }

            return collection.AsEnumerable();
        }

        public IQueryable<T> FilterQueryable(Expression<Func<T, bool>> query, params string[] includes)
        {
            var collection = Context.Set<T>().Where(query);

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    collection = collection.Include(include);
                }
            }

            return collection;
        }

        public PagedList<T> Filter(Paging paging, Expression<Func<T, bool>> query, Sorting sorting = null)
        {
            var collection = Context.Set<T>()
                .Where(query)
                .OrderBy(sorting)
                .SelectPage(paging);

            return new PagedList<T>
            {
                Collection = collection.AsEnumerable(),
                TotalCount = Count(query),
            };
        }

        public PagedList<T> Filter(Paging paging, Expression<Func<T, bool>> query, Sorting sorting = null, params string[] includes)
        {
            var collection = Context.Set<T>()
                .Where(query)
                .OrderBy(sorting)
                .SelectPage(paging);

            if (includes?.Any() == true)
            {
                includes.ToList().ForEach(x => collection = collection.Include(x));
            }

            return new PagedList<T>
            {
                Collection = collection.AsEnumerable(),
                TotalCount = Count(query),
            };
        }

        public PagedList<T> Filter(SearchCriteria searchCriteria, params string[] includes)
        {
            var collection = LinqExtension.ConvertToLinq(Context.Set<T>(), searchCriteria, out var allFilteredCollection);

            if (includes?.Any() == true)
            {
                includes.ToList().ForEach(x => collection = collection.Include(x));
            }

            return new PagedList<T>
            {
                Collection = collection.ToList(),
                TotalCount = allFilteredCollection.Count(),
            };
        }

        public PagedList<T> Filter(SearchCriteria searchCriteria, Expression<Func<T, bool>> query, params string[] includes)
        {
            var collection = LinqExtension.ConvertToLinq(Context.Set<T>().Where(query), searchCriteria, out var allFilteredCollection);

            if (includes?.Any() == true)
            {
                includes.ToList().ForEach(x => collection = collection.Include(x));
            }

            return new PagedList<T>
            {
                Collection = collection.ToList(),
                TotalCount = allFilteredCollection.Count(),
            };
        }

        public PagedList<T> FilterEnumerable(SearchCriteria searchCriteria, Expression<Func<T, bool>> query, params string[] includes)
        {
            var pagingCriteria = searchCriteria.PageCriteria;
            searchCriteria.PageCriteria = null;

            var collection = LinqExtension.ConvertToLinq(Context.Set<T>().AsQueryable(), searchCriteria, out _);
            if (includes?.Any() == true)
            {
                includes.ToList().ForEach(x => collection = collection.Include(x));
            }

            var result = collection.ToList().AsQueryable().Where(query);
            var totalCount = result.Count();
            if (pagingCriteria != null && pagingCriteria.AllowPaging && pagingCriteria.PageNumber.HasValue && pagingCriteria.PageSize.HasValue)
            {
                result = result.Skip((pagingCriteria.PageNumber.Value) * pagingCriteria.PageSize.Value).Take(pagingCriteria.PageSize.Value);
            }

            return new PagedList<T>
            {
                Collection = result.ToList(),
                TotalCount = totalCount,
            };
        }

        public T FirstOrDefault(bool track = true)
        {
            return track ? Context.Set<T>().FirstOrDefault() : Context.Set<T>().AsNoTracking().FirstOrDefault();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> query, bool track = true)
        {
            return track ? Context.Set<T>().FirstOrDefault(query) : Context.Set<T>().AsNoTracking().FirstOrDefault(query);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> query, params string[] includes)
        {
            var collection = Context.Set<T>().Where(query);

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    collection = collection.Include(include);
                }
            }

            return collection.FirstOrDefault();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> query, bool track = true, params string[] includes)
        {
            var collection = Context.Set<T>().Where(query);

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    collection = collection.Include(include);
                }
            }

            return track ? collection.FirstOrDefault() : collection.AsNoTracking().FirstOrDefault();
        }

        public int Count()
        {
            return Context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> query)
        {
            return Context.Set<T>().Count(query);
        }

        public int Count(SearchCriteria searchCriteria, Expression<Func<T, bool>> query)
        {
            LinqExtension.ConvertToLinq(Context.Set<T>().Where(query), searchCriteria, out var allFilteredCollection);

            return allFilteredCollection.Count();
        }

        public bool Any()
        {
            return Context.Set<T>().Any();
        }

        public bool Any(Expression<Func<T, bool>> query)
        {
            return Context.Set<T>().Any(query);
        }

        

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }
    }
}