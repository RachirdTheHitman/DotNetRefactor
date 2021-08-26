using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RefactorThis.Api.Models;

namespace RefactorThis.Api.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [Range(1, 5000)]
        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public List<ProductOption> ProductOptions { get; set; }
    }
}