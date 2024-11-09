using AFF_BE.Core.Data;
using AFF_BE.Core.ISeedWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Data.SeedWorks
{
    public class RepositoryQueryBase<T, K> : IRepositoryQueryBase<T, K>
         where T : EntityBase<K>
    {
        protected readonly AffContext _dbContext;
        public RepositoryQueryBase(AffContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IQueryable<T> FindAll(bool trackChanges = false)
        {
            return !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();
        }
        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
        {
            return !trackChanges
                ? _dbContext.Set<T>().Where(expression).AsNoTracking()
                : _dbContext.Set<T>().Where(expression);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public async Task<T?> GetByIdAsync(K id)
        {
            return await FindByCondition(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
        }
        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindByCondition(x => x.Id.Equals(id), false, includeProperties)
                .FirstOrDefaultAsync();
        }

        //public string RemoveVietnameseAccent(string input)
        //{
        //    using (var connection = _dbContext.Database.GetDbConnection())
        //    {
        //        connection.Open();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "SELECT dbo.RemoveVietnameseAccent(@input)";
        //            command.Parameters.Add(new SqlParameter("@input", SqlDbType.NVarChar) { Value = input });


        //            return (string)command.ExecuteScalar();
        //        }
        //    }
        //}

    }

}
