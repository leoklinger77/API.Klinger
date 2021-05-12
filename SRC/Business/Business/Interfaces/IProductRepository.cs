using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> FindAllProductsPerSupplier(Guid fornecedorId);
        Task<IEnumerable<Product>> FindAllProductsSupplier();
        Task<Product> FindProductSupplier(Guid id);
    }
}
