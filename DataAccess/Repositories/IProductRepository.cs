using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> ListProduct();
        Task<Product> GetProductById(int id);
        Task Insert(Product product);
        void Update(Product product);
        Task Delete(int id);
    }
}
