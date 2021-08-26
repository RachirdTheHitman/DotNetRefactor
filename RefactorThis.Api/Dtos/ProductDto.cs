using System;
using System.Collections.Generic;
using RefactorThis.Api.Models;

namespace RefactorThis.Api.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

    }
}