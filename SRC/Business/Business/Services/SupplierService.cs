using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }

        public async Task<bool> Insert(Supplier supplier)
        {
            if (!RunValidation(new SupplierValidation(), supplier)
                || !RunValidation(new AddressValidation(), supplier.Address)) return false;

            if (_supplierRepository.Find(f => f.Document == supplier.Document).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado.");
                return false;
            }

            await _supplierRepository.Add(supplier);
            return true;
        }

        public async Task<bool> Remove(Guid id)
        {
            if (_supplierRepository.FindSupplierAddress(id).Result.Products.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");
                return false;
            }

            var endereco = await _addressRepository.FindAlls(id);

            if (endereco != null)
            {
                await _addressRepository.Remove(endereco.Id);
            }

            await _supplierRepository.Remove(id);
            return true;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            if (!RunValidation(new SupplierValidation(), supplier)) return false;

            if (_supplierRepository.Find(f => f.Document == supplier.Document && f.Id != supplier.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento infomado.");
                return false;
            }   
            await _supplierRepository.Update(supplier);
            return true;
        }

        public async Task UpdateAddress(Address address)
        {
            if (!RunValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }
    }
}
