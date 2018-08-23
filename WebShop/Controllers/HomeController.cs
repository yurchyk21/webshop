using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DAL.Abstract;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        //public ApplicationDbContext MyDbContext
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Get<ApplicationDbContext>();
        //    }
        //}
        public ActionResult Index()
        {
            //int mycount = _userService.GetCountUsers();
            ViewBag.RoleId = 0;// _userService.AddRole("Admin");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}