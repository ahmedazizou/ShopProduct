using ProductShop.API.Data.Domains;
using ProductShop.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.API.Data.Repositories
{
    public interface IUserRepo
    {
        void RegisterUser(User user);
        void RegisterUserAdmin(User user);
        User Login(LoginModel loginModel);
        IEnumerable<User> GetUsers();
        User GetUserById(int id);

        bool SaveChanges();
        bool CheckUserAlreadyExist(User user);

        void UpdateUser(User user);
        void DeleteUser(User user);

    }
}
