using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BdempresaContext _context;
        public UnitOfWork(BdempresaContext context, string connectionString)
        {
            _context = context;
            ProductBusiness = new ProductBusiness(_context, connectionString);
            UserBusiness = new UserBusiness(_context);
        }
        public IProductBusiness ProductBusiness { get; private set; }

      
        public IUserBusiness UserBusiness { get; private set; }

        

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
