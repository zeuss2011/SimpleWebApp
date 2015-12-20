using Microsoft.Practices.Unity;
using SimpleWebApp.WebServices.Logger;
using System;
using System.IO;
using System.Reflection;

namespace SimpleWebApp.WebServices
{
    public static class UnityInjector
    {
        public static UnityContainer Container { get; set; }

        public static void ConfigureMe(Type type, object ob)
        {
            Container.BuildUp(type, ob);
        }
    }
}