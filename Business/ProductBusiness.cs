using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class ProductBusiness(DbContext context, string connectionString) : IProductBusiness
    {
        protected IProductRepository _productRepository { get; set; } = new ProductRepository(context, connectionString);

        public async Task<List<Product>> ListProduct()
        {
            return await _productRepository.ListProduct();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task Insert(Product product)
        {
            await _productRepository.Insert(product);
        }
        public  void Update(Product product)
        {
            _productRepository.Update(product);
        }
        public async Task Delete(int id)
        {
            await _productRepository.Delete(id);
        }
    }
}
