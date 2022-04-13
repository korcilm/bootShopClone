using bootShop.DataAccess.Data;
using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Dapper;

namespace bootShop.DataAccess.Repositories
{
    public class DpProductRepository : IProductRepository
    {
        
        string _tableName="dbo.Products";
        private readonly string _connectionString = "Server=DESKTOP-4LEK61G\\SQLEXPRESS;Initial Catalog=bootCampShopCloneDb;Integrated Security=True";

        public async Task<int> Add(Product entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"INSERT INTO {_tableName} VALUES ('{entity.Name}', {(double)entity.Price}, {(double)entity.Discount}, '{entity.Descriptipn}',{entity.CategoryId},'{entity.CreatedDate}','{entity.ModifiedDate}', '{entity.ImageUrl}')";
            await connection.ExecuteAsync(query);
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id = '{id}'");
        }

        public async Task<IList<Product>> GetAllEntities()
        {            
            await using var connection = new SqlConnection(_connectionString);
            return (await connection.QueryAsync<Product>($"SELECT * FROM {_tableName}")).ToList();
        }

        public async Task<Product> GetEntityById(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstAsync<Product>($"SELECT * FROM {_tableName} WHERE = '{id}'");
        }
       
        public async Task<IList<Product>> SearchProductsByName(string name)
        {
            await using var connection = new SqlConnection(_connectionString);
            return (IList<Product>)await connection.QueryAsync<Product>($"SELECT Name FROM {_tableName} WHERE Name LIKE '%{name}%'");                
        }

        public async Task<int> Update(Product entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($"UPDATE {_tableName} SET {nameof(entity.Name)} = '{entity.Name}', {nameof(entity.Price)} = {(double)entity.Price}," +
                $"{nameof(entity.Discount)} = {(double)entity.Discount}, {nameof(entity.Descriptipn)} = '{entity.Descriptipn}' , {nameof(entity.CategoryId)} = {entity.CategoryId}," +
                $" {nameof(entity.CreatedDate)} = '{entity.CreatedDate}', {nameof(entity.ModifiedDate)} = '{entity.ModifiedDate}', {nameof(entity.ImageUrl)} = '{entity.ImageUrl}' WHERE Id = '{entity.Id}'");
            return entity.Id;
        }

        public Task<bool> IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetEntityByIdSenkron(int id)
        {
            throw new NotImplementedException();
        }
    }
}
