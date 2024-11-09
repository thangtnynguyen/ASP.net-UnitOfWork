using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AFF_BE.Core.ISeedWorks;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using AFF_BE.Core.Data;
using System.Collections;
using AFF_BE.Core.Exceptions;
using AFF_BE.Core.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace AFF_BE.Data.SeedWorks
{
    public class RepositoryBase<T, Key> : IRepositoryBase<T, Key> where T : EntityBase<Key>

    {
        private readonly DbSet<T> _dbSet;
        protected readonly AffContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RepositoryBase(AffContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbSet = context.Set<T>();
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetByIdAsync(Key id)
        {
            return await _dbSet.FindAsync(id);
        }
        #region ignore
        //public void Add(T entity)
        //{
        //    _dbSet.AddAsync(entity);
        //}
        //public void AddRange(IEnumerable<T> entities)
        //{
        //    _dbSet.AddRange(entities);
        //}
        //public void Remove(T entity)
        //{
        //    _dbSet.Remove(entity);
        //}
        //public void RemoveRange(IEnumerable<T> entities)
        //{
        //    _dbSet.RemoveRange(entities);
        //}
        #endregion

        public async Task<Key> CreateAsync(T entity)
        {
            T? exist = _dbContext.Set<T>().Find(entity.Id);
            if (exist != null) { throw new Exception("Record for create already exist"); }

            PropertyInfo? propertyInfoCreateAt = entity.GetType().GetProperty("CreatedAt");
            if (propertyInfoCreateAt != null)
            {
                entity.CreatedAt = DateTime.Now;
            }

            PropertyInfo? propertyInfoUpdateAt = entity.GetType().GetProperty("UpdatedAt");
            if (propertyInfoUpdateAt != null)
            {
                entity.UpdatedAt = DateTime.Now;
            }

            PropertyInfo? propertyInfoIsDeleted = entity.GetType().GetProperty("IsDeleted");
            if (propertyInfoCreateAt != null)
            {
                entity.IsDeleted = false;
            }


            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                var name = httpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;
                var id = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

                if (!string.IsNullOrEmpty(id))
                {
                    PropertyInfo? propertyInfoCreatedBy = entity.GetType().GetProperty("CreatedBy");
                    if (propertyInfoCreatedBy != null)
                    {
                        if (int.TryParse(id, out var createdById))
                        {
                            propertyInfoCreatedBy.SetValue(entity, createdById);
                        }
                        else
                        {
                            propertyInfoCreatedBy.SetValue(entity, null); 
                        }
                    }

                    PropertyInfo? propertyInfoCreatedName = entity.GetType().GetProperty("CreatedName");
                    if (propertyInfoCreatedName != null)
                    {
                        propertyInfoCreatedName.SetValue(entity, name);
                    }
                }
            }

            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entity.Id;
        }
        public async Task<IList<Key>> CreateRangeAsync(IEnumerable<T> entities)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            foreach (var entity in entities)
            {
                T? exist = _dbContext.Set<T>().Find(entity.Id);
                if (exist != null)
                {
                    throw new Exception($"Record with Id {entity.Id} already exists.");
                }

                PropertyInfo? propertyInfoCreateAt = entity.GetType().GetProperty("CreatedAt");
                if (propertyInfoCreateAt != null)
                {
                    propertyInfoCreateAt.SetValue(entity, DateTime.Now);
                }

                PropertyInfo? propertyInfoUpdateAt = entity.GetType().GetProperty("UpdatedAt");
                if (propertyInfoUpdateAt != null)
                {
                    propertyInfoUpdateAt.SetValue(entity, DateTime.Now);
                }

                if (httpContext != null)
                {
                    var name = httpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;
                    var id = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

                    if (!string.IsNullOrEmpty(id))
                    {
                        PropertyInfo? propertyInfoCreatedBy = entity.GetType().GetProperty("CreatedBy");
                        if (propertyInfoCreatedBy != null)
                        {
                            if (int.TryParse(id, out var createdById))
                            {
                                propertyInfoCreatedBy.SetValue(entity, createdById);
                            }
                            else
                            {
                                propertyInfoCreatedBy.SetValue(entity, null);
                            }
                        }

                        PropertyInfo? propertyInfoCreatedName = entity.GetType().GetProperty("CreatedName");
                        if (propertyInfoCreatedName != null)
                        {
                            propertyInfoCreatedName.SetValue(entity, name);
                        }

                        PropertyInfo? propertyInfoIsDeleted = entity.GetType().GetProperty("IsDeleted");
                        if (propertyInfoCreateAt != null)
                        {
                            entity.IsDeleted = false;
                        }
                    }
                }
            }

            await _dbContext.Set<T>().AddRangeAsync(entities);
            await SaveChangesAsync();

            return entities.Select(x => x.Id).ToList();
        }


        public async Task UpdateAsync(T update)
        {
            if (_dbContext.Entry(update).State == EntityState.Unchanged) return;
            T? exist = _dbContext.Set<T>().Find(update.Id);
            if (exist == null) { throw new Exception("Record for update not found"); }
            update.UpdatedAt = DateTime.Now;
            update.CreatedAt = exist.CreatedAt;
            PropertyInfo? propertyVersion = exist.GetType().GetProperty("Version");
            if (propertyVersion != null)
            {
                object? valueNewUpdate = propertyVersion.GetValue(update);
                object? valueOldUpdate = propertyVersion.GetValue(exist);
                if (Comparer.Default.Compare(valueNewUpdate, valueOldUpdate) == 0)
                {
                    int plusVersion = Convert.ToInt32(valueNewUpdate) + 1;
                    propertyVersion.SetValue(update, plusVersion);
                }
                else
                {
                    throw new OldVersionException("The version is old");
                }
            }
           
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                var name = httpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;
                var id = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

                if (!string.IsNullOrEmpty(id))
                {
                    PropertyInfo? propertyInfoUpdatedBy = update.GetType().GetProperty("UpdatedBy");
                    if (propertyInfoUpdatedBy != null)
                    {
                        if (int.TryParse(id, out var createdById))
                        {
                            propertyInfoUpdatedBy.SetValue(update, createdById);
                        }
                        else
                        {
                            propertyInfoUpdatedBy.SetValue(update, null);
                        }
                    }

                    PropertyInfo? propertyInfoCreatedName = update.GetType().GetProperty("UpdatedName");
                    if (propertyInfoCreatedName != null)
                    {
                        propertyInfoCreatedName.SetValue(update, name);
                    }
                }
            }
            _dbContext.Entry(exist).CurrentValues.SetValues(update);
            await SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            foreach (var entity in entities)
            {
                if (_dbContext.Entry(entity).State == EntityState.Unchanged) continue;

                T? exist = _dbContext.Set<T>().Find(entity.Id);
                if (exist == null)
                {
                    throw new Exception($"Record with ID {entity.Id} not found for update.");
                }

                PropertyInfo? propertyInfoUpdatedAt = entity.GetType().GetProperty("UpdatedAt");
                if (propertyInfoUpdatedAt != null)
                {
                    propertyInfoUpdatedAt.SetValue(entity, DateTime.Now);
                }

                PropertyInfo? propertyInfoCreatedAt = entity.GetType().GetProperty("CreatedAt");
                if (propertyInfoCreatedAt != null)
                {
                    propertyInfoCreatedAt.SetValue(entity, exist.CreatedAt);
                }

                PropertyInfo? propertyVersion = entity.GetType().GetProperty("Version");
                if (propertyVersion != null)
                {
                    object? valueNewUpdate = propertyVersion.GetValue(entity);
                    object? valueOldUpdate = propertyVersion.GetValue(exist);
                    if (Comparer.Default.Compare(valueNewUpdate, valueOldUpdate) == 0)
                    {
                        int plusVersion = Convert.ToInt32(valueNewUpdate) + 1;
                        propertyVersion.SetValue(entity, plusVersion);
                    }
                    else
                    {
                        throw new OldVersionException("The version is old");
                    }
                }

                if (httpContext != null)
                {
                    var name = httpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;
                    var id = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

                    if (!string.IsNullOrEmpty(id))
                    {
                        PropertyInfo? propertyInfoUpdatedBy = entity.GetType().GetProperty("UpdatedBy");
                        if (propertyInfoUpdatedBy != null)
                        {
                            if (int.TryParse(id, out var createdById))
                            {
                                propertyInfoUpdatedBy.SetValue(entity, createdById);
                            }
                            else
                            {
                                propertyInfoUpdatedBy.SetValue(entity, null);
                            }
                        }

                        PropertyInfo? propertyInfoUpdatedName = entity.GetType().GetProperty("UpdatedName");
                        if (propertyInfoUpdatedName != null)
                        {
                            propertyInfoUpdatedName.SetValue(entity, name);
                        }
                    }
                }

                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            }

            await SaveChangesAsync();
        }


        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await SaveChangesAsync();
        }


        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }
        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        public Task RollbackTransactionAsync()
        {
            return _dbContext.Database.RollbackTransactionAsync();
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
