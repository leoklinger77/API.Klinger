using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Product>> FindAllProductsPerSupplier(Guid fornecedorId)
        {
            return await Find(p => p.SupplierId == fornecedorId);
        }

        public async Task<IEnumerable<Product>> FindAllProductsSupplier()
        {
            return await DbSet.AsNoTracking().Include(f => f.Supplier)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product> FindProductSupplier(Guid id)
        {
            return await DbSet.AsNoTracking().Include(f => f.Supplier)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
