using Api.Klinger.Controllers;
using Api.Klinger.Extensions;
using Api.Klinger.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Klinger.v1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/supplier")]
    public class SupplierController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IAddressRepository _addressRepository;
        public SupplierController(INotifier notifier, ISupplierRepository supplier, ISupplierService supplierService,
                                  IMapper mapper, IAddressRepository addressRepository, IUser user)
                                        : base(notifier, mapper, user)
        {
            _supplierRepository = supplier;
            _supplierService = supplierService;
            _addressRepository = addressRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> FindAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.FindAlls()));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<SupplierViewModel>> FindById(Guid id)
        {
            var db = _mapper.Map<SupplierViewModel>(await _supplierRepository.FindSupplierProductAddress(id));
            if (db == null)
            {
                ErrorNotifier("Fornecedor não encontrado");
                return CustomResponse();
            }
            return CustomResponse(db);
        }

        [ClaimsCustomAuthorize("Fornecedor", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Insert(SupplierViewModel supplierViewModelviewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _supplierService.Insert(_mapper.Map<Supplier>(supplierViewModelviewModel));
            supplierViewModelviewModel.Address.SupplierId = supplierViewModelviewModel.Id;
            return CustomResponse(supplierViewModelviewModel);
        }

        [ClaimsCustomAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, SupplierViewModel supplierViewModelviewModel)
        {
            if (id != supplierViewModelviewModel.Id)
            {
                ErrorNotifier("O Id informado não é o mesmo que foi passado na query");
                return CustomResponse(supplierViewModelviewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Update(_mapper.Map<Supplier>(supplierViewModelviewModel));

            return CustomResponse(supplierViewModelviewModel);

        }

        [ClaimsCustomAuthorize("Fornecedor", "Remover")]
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<SupplierViewModel>> Delete(Guid id)
        {
            var supplier = _mapper.Map<Supplier>(await _supplierRepository.FindSupplierProductAddress(id));

            if (supplier == null)
            {
                ErrorNotifier("Fornecedor não encontrado");
                return CustomResponse();
            }
            await _supplierService.Remove(id);
            return CustomResponse();
        }

        [HttpGet("FindByAddress/{id:Guid}")]
        public async Task<ActionResult> FindByAddress(Guid id)
        {
            return CustomResponse(await _addressRepository.FindById(id));
        }

        [HttpPut("UpdateAddress/{id:Guid}")]
        public async Task<ActionResult> UpdateAddress(Guid Id, AddressViewModel addressViewModel)
        {
            if (Id != addressViewModel.Id)
            {
                ErrorNotifier("O Id informado não é o mesmo que foi passado na query");
                return CustomResponse(addressViewModel);
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(addressViewModel));

            return CustomResponse(addressViewModel);
        }
    }
}
