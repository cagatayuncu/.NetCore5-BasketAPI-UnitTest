using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Data.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class where TKey : struct
    {
        public readonly CartDbContext Context;
        public readonly DbSet<TEntity> DbSet;
        public IQueryable<TEntity> Queryable;

        public RepositoryBase(CartDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
            Queryable = DbSet;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, bool autoSave = false)
        {
            await Context.AddAsync(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }

            return entity;
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return Queryable;
        }

        public virtual async Task DeleteAsync(TEntity entity, bool autoSave = false)
        {
            DbSet.Remove(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteAsync(TKey id, bool autoSave = false)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity, autoSave);
            }
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            return AttachIncludes(includes).FirstOrDefaultAsync(predicate);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false)
        {
            if (includeDetails)
            {
                var navigations = Context.Model.FindEntityType(typeof(TEntity)).GetNavigations();
                if (navigations != null && navigations.Any())
                {
                    foreach (var property in navigations)
                    {
                        Queryable = Queryable.Include(property.Name);
                    }
                }
            }

            return Queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate,
            params string[] includes)
        {
            var entity = await AttachIncludes(includes).AsNoTracking().FirstOrDefaultAsync(predicate);

            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        private async Task IncludeDetails(TEntity model)
        {
            var navigations = Context.Model.FindEntityType(model.GetType()).GetNavigations();
            if (navigations != null && navigations.Any())
            {
                foreach (var property in navigations)
                {
                    var propertyType = property.PropertyInfo.PropertyType;
                    if (propertyType.IsGenericType)
                    {
                        await Context.Entry(model).Collection(property.Name).LoadAsync();
                    }
                    else
                    {
                        await Context.Entry(model).Reference(property.Name).LoadAsync();
                    }
                }
            }
        }

        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = false)
        {
            var model = await DbSet.FindAsync(id);
            if (model == null)
            {
                return null;
            }

            if (includeDetails)
            {
                await IncludeDetails(model);
            }

            return model;
        }

        public virtual async Task<List<TEntity>> GetListAsync()
        {
            var model = await DbSet.ToListAsync();

            return model;
        }


        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false)
        {
            DbSet.Update(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }

            return entity;
        }




        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        private IQueryable<TEntity> AttachIncludes(params string[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return Queryable;
            }


            foreach (var include in includes)
            {
                Queryable = Queryable.Include(include);
            }

            return Queryable;
        }
    }
}
    
