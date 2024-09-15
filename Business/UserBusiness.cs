using DataAccess.Models;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserBusiness(DbContext context) : IUserBusiness
    {
        protected IUserRepository _userRepository { get; set; } = new UserRepository(context);
        public async Task<User> ValidateUser(User user)
        {
            return await _userRepository.ValidateUser(user);
        }


    }
}
