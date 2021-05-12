using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        //private readonly IUser _user;

        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
            //_user = user;
        }

        public async Task Add(Product product)
        {
            if (!RunValidation(new ProductValidation(), product)) return;

            if (product.Supplier != null) product.Supplier = null;            

            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!RunValidation(new ProductValidation(), product)) return;
            await _productRepository.Update(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
            await _productRepository.Remove(id);
        }
    }
}
