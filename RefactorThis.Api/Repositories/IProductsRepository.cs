using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RefactorThis.Api.Models;

namespace RefactorThis.Api.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetProductAsync(Guid productId);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task<IEnumerable<ProductOption>> GetProductOptionsAsync(Guid productId);
        Task<ProductOption> GetProductOptionAsync(Guid optionId);
        Task AddProductOptionAsync(ProductOption productOption);
        Task UpdateProductOptionAsync(ProductOption productOption);
        Task DeleteProductOptionAsync(Guid optionId);
    }
}