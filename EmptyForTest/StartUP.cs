using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using EmptyForTest.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(EmptyForTest.StartUP))]

namespace EmptyForTest
{
    public class StartUP
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.CreatePerOwinContext(() => new User_Context());
            app.CreatePerOwinContext<UserStore<USER>>((opt, cont) => new UserStore<USER>(cont.Get<User_Context>()));
            app.CreatePerOwinContext<UserManager<USER>>((opt, cont) => new UserManager<USER>(new UserStore<USER>(cont.Get<User_Context>())));

            app.CreatePerOwinContext<SignInManager<USER, string>>((opt, cont) => new SignInManager<USER, string>(cont.Get<UserManager<USER>>(), cont.Authentication));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            });

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
