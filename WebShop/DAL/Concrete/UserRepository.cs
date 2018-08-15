using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.DAL.Concrete
{
    public class UserRepository
    {
        private readonly ApplicationUserManager _userManager;
        public UserRepository(ApplicationUserManager userManager)
        {
            _userManager = userManager;
            int count = _userManager.Users.Count();// FindByEmail("novakvova@gmail.com")
        }
    }
}