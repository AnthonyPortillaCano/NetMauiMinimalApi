using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IProductBusiness
    {
        Task<List<Product>> ListProduct();
        Task<Product> GetProductById(int id);

        Task Insert(Product product);
        void Update(Product product);

        Task Delete(int id);
    }
}
