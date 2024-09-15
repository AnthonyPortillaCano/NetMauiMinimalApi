using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAccess.Repositories
{
    public class ProductRepository(DbContext context, string connectionString) : RepositoryEF<Product>(context), IProductRepository
    {
        protected readonly string _connectionString = connectionString;

        public IRepositoryEF<Product> repositoryEF { get; set; } = new RepositoryEF<Product>(context);
        public async Task<List<Product>> ListProduct()
        {
            using var conexion = new SqlConnection(_connectionString);
            try
            {
                await conexion.OpenAsync();
                var result = await conexion.QueryAsync<Product>("SP_LIST_PRODUCT", commandType: CommandType.StoredProcedure);
                await conexion.CloseAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Product> GetProductById(int id)
        {
            using var conexion = new SqlConnection(_connectionString);
            var product = new Product();
            try
            {
                await conexion.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);
                product = await conexion.QueryFirstAsync<Product>("SP_GET_PRODUCT_BY_ID", parameters, commandType: CommandType.StoredProcedure);
                await conexion.CloseAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task Insert(Product product)
        {
            await repositoryEF.Insert(product);
        }
        public void Update(Product product)
        {
            repositoryEF.Update(product);
        }
        public async Task Delete(int id)
        {
            await repositoryEF.Delete(d => d.Id == id);
        }
    }
}
