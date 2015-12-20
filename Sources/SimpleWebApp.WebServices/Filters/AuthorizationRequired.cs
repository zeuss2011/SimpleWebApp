using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using SimpleWebApp.Common;
using Microsoft.Practices.Unity;
using SimpleWebApp.WebServices.Logger;
using System;
using System.Linq;
using System.Net.Http;
using System.Net;

namespace SimpleWebApp.WebServices.Filters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string TokenHeaderKey = "Token";

        #region Ctor

        private void ConfigUnity()
        {
            //Configuration Unity
            UnityInjector.ConfigureMe(typeof(AuthorizationRequiredAttribute), this);
        }

        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public AuthorizationRequiredAttribute()
        {
            ConfigUnity();
        }

        #endregion

        #region Services

        [Dependency]
        public ITokenService TokenService { get; set; }

        #endregion Services

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (filterContext.Request.Headers.Contains(TokenHeaderKey))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(TokenHeaderKey).FirstOrDefault();

                // Validate Token
                if (tokenValue != null && !TokenService.ValidateToken(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                    filterContext.Response = responseMessage;
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}