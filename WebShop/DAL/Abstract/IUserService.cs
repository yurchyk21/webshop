using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DAL.Abstract
{
    public interface IUserService
    {
        int GetCountUsers();
        int AddRole(string name);
    }
}
