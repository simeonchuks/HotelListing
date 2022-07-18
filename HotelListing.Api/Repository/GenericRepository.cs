﻿using HotelListing.Api.Configurations.Pagination;
using HotelListing.Api.Data;
using HotelListing.Api.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities) => 
            _db.RemoveRange(entities);

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            //Check if what is pass is not null then loop through and include all of then in the loop
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression); //This means query the db get me a data were condition. EG: Give ne the list of hotel were country is Nigeria. thats
                                                // Thats what expression represent. 
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task Insert(T entity) => 
            await _db.AddAsync(entity);

        public async Task InsertRange(IEnumerable<T> entities) => 
            await _db.AddRangeAsync(entities);

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
