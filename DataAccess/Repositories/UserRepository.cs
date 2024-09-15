using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository(DbContext context): RepositoryEF<User>(context), IUserRepository
    {
      
        public IRepositoryEF<User> repositoryEF { get; set; } = new RepositoryEF<User>(context);
        
        public async Task<User> ValidateUser(User user)
        {
            return await repositoryEF.Obtener<User>(a=>a.Nombre==user.Nombre && a.Clave==user.Clave);
        }
    }
}
