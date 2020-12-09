using ProductShop.API.Data.Domains;
using ProductShop.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.API.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ProductShopContext _context;

        public UserRepo(ProductShopContext context)
        {
            _context = context;

        }

        public bool CheckUserAlreadyExist(User user)
        {
            var isExist = _context.Users.Any(u => u.Email == user.Email);
            return isExist;
        }

       

        public IEnumerable<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User Login(LoginModel loginModel)
        {
            var loggedInuser = _context.Users.SingleOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            return loggedInuser;
        }

        public void RegisterUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.IsClient = true;
            _context.Users.Add(user);
        }

        public void RegisterUserAdmin(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);

        }

        public void UpdateUser(User user)
        {
        }
        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Remove(user);
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            return user;
        }
    }
}
