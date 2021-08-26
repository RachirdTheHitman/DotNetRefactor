using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefactorThis.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public List<ProductOption> ProductOptions { get; set; }

        // public DateTimeOffset CreatedDate { get; set; }

    }
}