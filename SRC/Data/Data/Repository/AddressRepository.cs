using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DataContext context) : base(context) { }

        public async Task<Address> FindAlls(Guid supplierId)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.SupplierId == supplierId);
        }
    }
}
