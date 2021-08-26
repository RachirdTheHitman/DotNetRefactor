using System;
using System.ComponentModel.DataAnnotations;

namespace RefactorThis.Api.Dtos
{
    public class UpdateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(1, 5000)]
        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

    }
}