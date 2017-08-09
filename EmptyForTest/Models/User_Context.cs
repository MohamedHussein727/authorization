using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmptyForTest.Models
{
    public class User_Context: IdentityDbContext <USER>
    {
        public User_Context() : base("User_Context") { }


    }
}