using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Api.Dtos;
using RefactorThis.Api.Models;
using RefactorThis.Api.Repositories;
using RefactorThis.Api.Types;

namespace RefactorThis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository repository;

        public ProductsController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        // GET /products
        [HttpGet]
        public async Task<ProductsResponse> GetProductsAsync(string name = null)
        {
            var products = (await repository.GetProductsAsync()).Select(product => product.AsDto());

            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            return new ProductsResponse(products);
        }

        // GET /products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductAsync(Guid id)
        {
            var product = await repository.GetProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return product.AsDto();
        }

        // POST /products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                DeliveryPrice = createProductDto.DeliveryPrice
            };

            await repository.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductAsync), new { id = product.Id }, product.AsDto());
        }


        //PUT /products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductAsync(Guid id, UpdateProductDto productDto)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.DeliveryPrice = productDto.DeliveryPrice;

            await repository.UpdateProductAsync(existingProduct);

            return NoContent();

        }

        //PUT /products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(Guid id)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            await repository.DeleteProductAsync(id);

            return NoContent();
        }

        //GET /products/{id}/options
        [HttpGet("{id}/options")]
        public async Task<ProductOptionsResponse> GetProductOptionsAsync(Guid id)
        {
            var options = (await repository.GetProductOptionsAsync(id)).Select(option => option.AsDto());

            return new ProductOptionsResponse(options);
        }

        //GET /products/{id}/options/{optionId}
        [HttpGet("{id}/options/{optionId}")]
        public async Task<ActionResult<ProductOptionDto>> GetProductOptionAsync(Guid id, Guid optionId)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            var productOption = await repository.GetProductOptionAsync(optionId);

            return productOption.AsDto();
        }

        //POST /products/{id}/options
        [HttpPost("{id}/options")]
        public async Task<ActionResult<ProductOptionDto>> AddProductOptionAsync(Guid id, AddProductOptionDto addProductOptionDto)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            ProductOption option = new ProductOption()
            {
                Id = Guid.NewGuid(),
                Name = addProductOptionDto.Name,
                ProductId = id,
                Description = addProductOptionDto.Description,
            };

            await repository.AddProductOptionAsync(option);

            return CreatedAtAction(nameof(GetProductOptionAsync), new { id = option.ProductId, optionId = option.Id }, option.AsDto());
        }

        //PUT /products/{id}/options/{optionId}
        [HttpPut("{id}/options/{optionId}")]
        public async Task<ActionResult> UpdateProductOptionAsync(Guid id, Guid optionId, UpdateProductOptionDto option)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            var existingProductOption = await repository.GetProductOptionAsync(optionId);

            if (existingProductOption is null)
            {
                return NotFound();
            }

            existingProductOption.Name = option.Name;
            existingProductOption.Description = option.Description;

            await repository.UpdateProductOptionAsync(existingProductOption);

            return NoContent();
        }

        //DELETE /products/{id}/options/{optionId}
        [HttpDelete("{id}/options/{optionId}")]
        public async Task<ActionResult> DeleteProductOptionAsync(Guid id, Guid optionId)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            var existingProductOption = await repository.GetProductOptionAsync(optionId);

            if (existingProductOption is null)
            {
                return NotFound();
            }

            await repository.DeleteProductOptionAsync(optionId);

            return NoContent();
        }
    }
}