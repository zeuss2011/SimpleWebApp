using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebAppKatana
{
    public class OwinAuthDbContext : IdentityDbContext
    {
        public OwinAuthDbContext()
            : base("OwinAuthDbConnection")
        {
        }
    }  
}