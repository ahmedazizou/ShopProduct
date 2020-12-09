using Microsoft.AspNetCore.Mvc;
using ProductShop.API.Data.Domains;
using ProductShop.API.Data.Repositories;
using ProductShop.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthencationController : ControllerBase
    {
        private readonly IUserRepo _repository;

        public AuthencationController(IUserRepo repository)
        {
            _repository = repository;

        }

        [HttpGet]
        [Route("users")]
        public ActionResult<IEnumerable<User>> Users()
        {
            var users = _repository.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<User> Login(LoginModel model)
        {
           
            var user = _repository.Login(model);
            if (user == null)
            {
                return NotFound("Invalid user name or password.");
            }
            else
            {
                return user;

            }
        }
        [HttpPost]
        [Route("register")]
        public ActionResult<string> Register(User user)
        {
            var isAlreadyExist = _repository.CheckUserAlreadyExist(user);
            if (!isAlreadyExist)
            {
                _repository.RegisterUser(user);
                _repository.SaveChanges();
                return Ok("User created successfully.");

            }
            else
            {
                return NotFound("User already exists. please use different email.");

            }




        }
        [HttpPost]
        [Route("registerAdmin")]
        public ActionResult<string> RegisterAdmin(User user)
        {
            var isAlreadyExist = _repository.CheckUserAlreadyExist(user);
            if (!isAlreadyExist)
            {
                _repository.RegisterUserAdmin(user);
                _repository.SaveChanges();
                return Ok("Admin is User created.");


            }
            else
            {
                return NotFound("User already exists. please use different email.");

            }



        }
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User user)
        {
            var userInDb = _repository.GetUserById(id);
            if (userInDb == null)
            {
                return NotFound();
            }
            userInDb.LastName = user.LastName;
            userInDb.Password = user.Password;
            userInDb.FirstName = user.FirstName;
            userInDb.Email = user.Email;
            userInDb.IsClient = user.IsClient;
            _repository.SaveChanges();

            return NoContent();

        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {

            var user = _repository.GetUserById(id);

            if (user != null)
            {
                return Ok(user);

            }
            return NotFound();

        }
        //Delete: api/User/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var userInDb = _repository.GetUserById(id);
            if (userInDb == null)
            {
                return NotFound();
            }
            _repository.DeleteUser(userInDb);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
