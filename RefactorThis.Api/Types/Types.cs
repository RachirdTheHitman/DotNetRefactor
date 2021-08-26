using System.Collections.Generic;
using RefactorThis.Api.Dtos;
using RefactorThis.Api.Models;

namespace RefactorThis.Api.Types
{
    public class ProductsResponse
    {
        public IEnumerable<ProductDto> Items { get; private set; }

        public ProductsResponse(IEnumerable<ProductDto> products)
        {
            Items = products;
        }
    }

    public class ProductOptionsResponse
    {
        public IEnumerable<ProductOptionDto> Items { get; private set; }

        public ProductOptionsResponse(IEnumerable<ProductOptionDto> options)
        {
            Items = options;
        }
    }
}