using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly DataContext _context;
        public readonly DbSet<TEntity> DbSet;

        public Repository(DataContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChanges();
        }

        public void Dispose()
        {
            _context?.DisposeAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> FindAlls()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> FindById(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public virtual async Task Remove(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.UpdateRange(entity);
            await SaveChanges();
        }
    }
}
