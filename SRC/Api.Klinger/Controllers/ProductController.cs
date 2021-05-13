using Api.Klinger.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Klinger.Controllers
{
    [Route("api/Product")]
    public class ProductController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductController(INotifier notifier, IMapper mapper, IProductService productService, IProductRepository productRepository) : base(notifier, mapper)
        {
            _productService = productService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            return CustomResponse(await _productRepository.FindAllProductsSupplier());
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> FindByProduct(Guid id)
        {
            return CustomResponse(await FindById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Insert(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + "_" + productViewModel.Image;

            if(!UpdateFile(productViewModel.ImagemUploud, imageName))
            {
                return CustomResponse(productViewModel);
            }
            productViewModel.Image = imageName;

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(productViewModel);
        }
        [RequestSizeLimit(700000000)]
        [HttpPost("image")]
        public async Task<ActionResult> Image(ProductImageViewModel productImage)
        {           

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            string imageName = Guid.NewGuid() + "";
            if (!await UpdateBig(productImage.ImagemUploud, imageName))
            {
                return CustomResponse(productImage);
            }
            productImage.Image = imageName + productImage.ImagemUploud.FileName;

            await _productService.Add(_mapper.Map<Product>(productImage));

            return CustomResponse(productImage);
        }


        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await FindById(id);
            if (product == null)
            {
                ErrorNotifier("Fornecedor não encontrado");
                return CustomResponse();
            }
            await _productService.Remover(id);
            return CustomResponse();
        }

        private bool UpdateFile(string file, string imgName)
        {            
            if (string.IsNullOrEmpty(file))
            {
                ErrorNotifier("Forneça uma imagem para este produto!");
                return false;
            }
            var imageDataByteArray = Convert.FromBase64String(file);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgName);

            if (System.IO.File.Exists(filePath))
            {
                ErrorNotifier("Já existe um arquivo com esse nome!");
                return false;
            }
            new Thread(() =>
            {
                System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            }).Start();            
            return true;
        }

        private async Task<bool> UpdateBig(IFormFile file, string imgName)
        {
            if (file == null || file.Length == 0)
            {
                ErrorNotifier("Forneça uma imagem para este produto!");
                return false;
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgName + file.FileName);
            if (System.IO.File.Exists(filePath))
            {
                ErrorNotifier("Já existe um arquivo com esse nome!");
                return false;
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        private async Task<ProductViewModel> FindById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.FindById(id));
        }
    }
}
