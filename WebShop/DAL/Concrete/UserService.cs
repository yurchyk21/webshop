using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DAL.Abstract;
using WebShop.Models;

namespace WebShop.DAL.Concrete
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        //private readonly ApplicationUserManager _userManager;
        public UserService(ApplicationDbContext context)
        {
            //_userManager = userManager;
            //int count = _userManager.Users.Count();// FindByEmail("novakvova@gmail.com")
            _context = context;
        }

        public int GetCountUsers()
        {
            return _context.Users.Count();
        }
    }
}