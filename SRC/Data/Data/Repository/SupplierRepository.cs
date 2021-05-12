using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataContext context) : base(context) { }

        public async Task<Supplier> FindSupplierAddress(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supplier> FindSupplierProductAddress(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(c => c.Products)
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
