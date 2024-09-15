using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IUserBusiness
    {
        Task<User> ValidateUser(User user);
    }
}
