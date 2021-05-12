using Business.Models;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProductService :IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remover(Guid id);
    }
}
