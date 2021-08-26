using System;
using Newtonsoft.Json;

namespace RefactorThis.Api.Models
{

    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        // public DateTimeOffset CreatedDate { get; set; }
        
    }
}