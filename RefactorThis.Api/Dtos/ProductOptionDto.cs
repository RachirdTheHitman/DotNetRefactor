using System;
using RefactorThis.Api.Models;

namespace RefactorThis.Api.Dtos
{
    public class ProductOptionDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        // public Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}