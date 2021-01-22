using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashewDocumentParser.Models.Infrastructure
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : ApplicationDbContext
    {
        private readonly TContext context;
        public Repository(TContext context)
        {
            this.context = context;
        }
        public TEntity Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);

            return entity;
        }

        public void Delete(Func<TEntity, bool> conditions)
        {
            var entities = context.Set<TEntity>().Where(conditions).ToList();
            context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public List<TEntity> GetMultiple(Func<TEntity, bool> conditions)
        {
            return context.Set<TEntity>().Where(conditions).ToList();
        }

        public TEntity GetSingle(Func<TEntity, bool> conditions)
        {
            return context.Set<TEntity>().Where(conditions).FirstOrDefault();
        }

        public TEntity Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
