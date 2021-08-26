using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using RefactorThis.Api.Models;
using RefactorThis.Api.Settings;

namespace RefactorThis.Api.Repositories
{
    public class SqliteProductsRepository : IProductsRepository
    {
        private readonly SqliteConnection conn;

        public SqliteProductsRepository()
        {
            conn = new SqliteConnection(SqliteSettings.ConnectionString);
        }

        public async Task AddProductOptionAsync(ProductOption productOption)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"insert into ProductOptions (id, name, description, productid) values ('{productOption.Id}', '{productOption.Name}', '{productOption.Description}', '{productOption.ProductId}')";

            Console.WriteLine(cmd.CommandText);

            await cmd.ExecuteNonQueryAsync();

            conn.Close();
        }

        public async Task CreateProductAsync(Product product)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"insert into Products (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})";

            await cmd.ExecuteNonQueryAsync();

            conn.Close();
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            List<ProductOption> Items = (List<ProductOption>)await GetProductOptionsAsync(productId);

            conn.Open();

            var cmd = conn.CreateCommand();

            foreach (var option in Items)
            {
                cmd.CommandText = $"delete from ProductOptions where id = '{option.Id}' collate nocase";

                await cmd.ExecuteNonQueryAsync();
            }

            conn.Close();
        }

        public async Task DeleteProductOptionAsync(Guid optionId)
        {
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = $"delete from ProductOptions where id = '{optionId}' collate nocase";

            await cmd.ExecuteNonQueryAsync();

            conn.Close();
        }

        public async Task<Product> GetProductAsync(Guid productId)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from Products where id = '{productId}' collate nocase";

            Console.WriteLine(cmd.CommandText);

            var rdr = await cmd.ExecuteReaderAsync();

            if (!rdr.Read())
            {
                System.Diagnostics.Debug.WriteLine("this is a dubug informamtion line");
                return null;
            }

            var product = new Product()
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                Price = decimal.Parse(rdr["Price"].ToString()),
                DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
            };

            conn.Close();

            return product;
        }

        public async Task<ProductOption> GetProductOptionAsync(Guid OptionId)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from productoptions where id = '{OptionId}' collate nocase";

            var rdr = await cmd.ExecuteReaderAsync();

            if (!rdr.Read())
            {
                return null;
            }

            var productOption = new ProductOption()
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
            };

            conn.Close();

            return productOption;
        }

        public async Task<IEnumerable<ProductOption>> GetProductOptionsAsync(Guid productId)
        {
            List<ProductOption> Items = new List<ProductOption>();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from ProductOptions where productId = '{productId}' collate nocase";

            var rdr = await cmd.ExecuteReaderAsync();
            while (rdr.Read())
            {
                var productOption = new ProductOption()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                    ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                };
                Items.Add(productOption);
            }

            conn.Close();

            return Items;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {

            List<Product> Items = new List<Product>();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Products";

            var rdr = await cmd.ExecuteReaderAsync();
            while (rdr.Read())
            {
                var product = new Product()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                    Price = decimal.Parse(rdr["Price"].ToString()),
                    DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
                };
                Items.Add(product);
            }

            conn.Close();

            return Items;
        }

        public async Task UpdateProductAsync(Product product)
        {

            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"update Products set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}' collate nocase";

            var rdr = await cmd.ExecuteNonQueryAsync();

            conn.Close();
        }

        public async Task UpdateProductOptionAsync(ProductOption ProductOption)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"update ProductOptions set name = '{ProductOption.Name}', description = '{ProductOption.Description}' where id = '{ProductOption.Id}' collate nocase";

            var rdr = await cmd.ExecuteNonQueryAsync();

            conn.Close();
        }
    }
}