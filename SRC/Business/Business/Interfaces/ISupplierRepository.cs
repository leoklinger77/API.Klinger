using Business.Models;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> FindSupplierAddress(Guid id);
        Task<Supplier> FindSupplierProductAddress(Guid id);

    }
}
