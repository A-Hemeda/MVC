using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Specifications;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(TEntity) == typeof(Product))
            { 
                // Must casting from genericList to Ienumerable because it return genericList and the base func return IEnumerable
                return (IEnumerable<TEntity>) await _context.Products.Include(P => P.Type).Include(P => P.Brand).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                // Must casting here [Force] : 
                return  await _context.Products.Where(P => P.Id == id as int?).Include(P => P.Type).Include(P => P.Brand).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity; // (Id) here is int and (id) here is Entity
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
             await _context.AddAsync(entity);
        }

        public  void UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            _context.Remove(entity);
        }
         

        // With Specifications :
        // To List 
        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications)
        {

           return await ApplySpec(specifications).ToListAsync();

        }
        // First Or Default 
        public async Task<TEntity> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpec(specifications).FirstOrDefaultAsync();
        }

        // to less the repeat the same Code : >> Refactoring <<
        // SpecificationEvaluator<TEntity,TKey>.GetQuery(_context.Set<TEntity>() , specifications) 

        private IQueryable<TEntity> ApplySpec(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specifications);
        }

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> specifications)
        {

            return await ApplySpec(specifications).CountAsync();
        }
    }
}
