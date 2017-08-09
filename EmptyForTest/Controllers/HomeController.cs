using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EmptyForTest.Models;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;

namespace EmptyForTest.Controllers
{
    public class HomeController : Controller
    {
        public UserStore<USER> myUser = new UserStore<USER>(new User_Context());
        public UserManager<USER> _um => HttpContext.GetOwinContext().Get<UserManager<USER>>();
        public SignInManager<USER,string> _sim => HttpContext.GetOwinContext().Get<SignInManager<USER,string>>();

        RoleStore<IdentityRole> ROLE;
        IdentityRole id;
        RoleManager<IdentityRole> Manger_ROLES;
        //um => HttpContext

        public HomeController()
        {
            //BOSS = new UserManager<USER>(myUser);
        }
        IdentityUser x = new IdentityUser();
        public void wtv()
        {
            id = new IdentityRole();
            id.Name = "Admin";
            ROLE = new RoleStore<IdentityRole>(new User_Context());
            Manger_ROLES = new RoleManager<IdentityRole>(ROLE);
            Manger_ROLES.Create(id);
        }
        public ActionResult Index()
        {
            //okay.Create()
            return View();
        }
        public ActionResult Create()
        {
            wtv();
            USER US = new USER()
            {
                Id = "10",
                UserName = "batman",
                Email = "mmmm@mmm.mmm",
            };
            _um.Create(US, "123456789");
            _um.AddToRole("10", "admin");
            return View();
        }
        public string Login()
        {
            //USER X = _um.FindByName("flash");

            SignInStatus SIS = _sim.PasswordSignIn("batman", "123456789", false, false);
            switch (SIS)
            {
                case SignInStatus.Failure:
                    return "failed";
                case SignInStatus.LockedOut:
                    return "Locked";
                case SignInStatus.RequiresVerification:
                    return "recuire validation";
                case SignInStatus.Success:
                    return "success";

            }
            return "NO THIS WAS SO WRONG";
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Check_Authority()
        {
            return View();
        }
    }
}