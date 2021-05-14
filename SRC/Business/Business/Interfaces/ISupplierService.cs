using Business.Models;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISupplierService : IDisposable
    {
        Task<bool> Insert(Supplier supplier);
        Task<bool> Update(Supplier supplier);
        Task<bool> Remove(Guid id);
        Task UpdateAddress(Address address);
    }
}
