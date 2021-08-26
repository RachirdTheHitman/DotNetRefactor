using RefactorThis.Api.Dtos;
using RefactorThis.Api.Models;

namespace RefactorThis.Api
{
    public static class Extensions
    {
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice,
            };
        }

        public static ProductOptionDto AsDto(this ProductOption productOption)
        {
            return new ProductOptionDto
            {
                Id = productOption.Id,
                Name = productOption.Name,
                // Product = productOption.Product,
                Description = productOption.Description,
                ProductId = productOption.ProductId
            };
        }
    }
}