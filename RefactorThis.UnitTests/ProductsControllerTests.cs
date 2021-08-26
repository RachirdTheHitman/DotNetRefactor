using System;
using Xunit;
using Moq;
using RefactorThis.Api.Repositories;
using RefactorThis.Api.Models;
using RefactorThis.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RefactorThis.Api.Dtos;
using FluentAssertions;
using System.Collections.Generic;
using RefactorThis.Api;
using System.Linq;

namespace RefactorThis.UnitTests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsRepository> repositoryStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetProductAsync_WithUnexistingProduct_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Product)null);

            var controller = new ProductsController(repositoryStub.Object);

            // Act
            var result = await controller.GetProductAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetProductAsync_WithExistingProduct_ReturnsExpectedItem()
        {
            // Arrange
            Product expectedProduct = CreateRandomProduct();

            repositoryStub.Setup(repo => repo.GetProductAsync(It.IsAny<Guid>())).
                ReturnsAsync(expectedProduct);

            var controller = new ProductsController(repositoryStub.Object);

            // Act
            var result = await controller.GetProductAsync(Guid.NewGuid());

            //Assert
            result.Value.Should().BeEquivalentTo(expectedProduct.AsDto());
        }

        // [Fact]
        // public async Task GetProductsAsync_WithExistingProducts_ReturnsAllProducts()
        // {
        //     // Arrange
        //     IEnumerable<Product> expectedProducts = new[] { CreateRandomProduct(), CreateRandomProduct(), CreateRandomProduct() };
        //     IEnumerable<ProductDto> ExpectedResult = new ProductsResponse(products);

        //     repositoryStub.Setup(repo => repo.GetProductsAsync())
        //         .ReturnsAsync(expectedProducts);

        //     var controller = new ProductsController(repositoryStub.Object);

        //     // Act
        //     var actualProducts = await controller.GetProductsAsync();

        //     // Assert
        //     actualProducts.Should().BeEquivalentTo(
        //         expectedProducts.Select(product => product.AsDto()),
        //         options => options.ComparingByMembers<ProductDto>()
        //     );
        // }

        private Product CreateRandomProduct()
        {
            return new ()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Price = rand.Next(2000),
                DeliveryPrice = rand.Next(20),
            };
        }
    }
}
