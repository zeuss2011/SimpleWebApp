using System.Threading;
using System.Web.Http.Controllers;
using SimpleWebApp.Common;
using Microsoft.Practices.Unity;
using SimpleWebApp.WebServices.Logger;
using System;

namespace SimpleWebApp.WebServices.Filters
{
    /// <summary>
    /// Custom Authentication Filter Extending basic Authentication
    /// </summary>
    public class ApiAuthenticationFilterAttribute : GenericAuthenticationFilterAttribute
    {
        #region Ctor

        private void ConfigUnity()
        {
            //Configuration Unity
            UnityInjector.ConfigureMe(typeof(ApiAuthenticationFilterAttribute), this);
        }

        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public ApiAuthenticationFilterAttribute()
        {
            ConfigUnity();
        }

        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public ApiAuthenticationFilterAttribute(bool isActive)
            : base(isActive)
        {
            ConfigUnity();
        }

        #endregion

        #region Services

        [Dependency]
        public IUtilisateurService UtilisateurService { get; set; }

        #endregion Services

        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var user = UtilisateurService.GetUtilisateurByLoginAndPassWord(username, password);
            if (user != null)
            {
                var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                    basicAuthenticationIdentity.UserId = user.Id;
                return true;
            }
            return false;
        }
    }
}