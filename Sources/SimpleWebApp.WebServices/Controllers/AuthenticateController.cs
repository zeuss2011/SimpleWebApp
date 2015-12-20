using Microsoft.Practices.Unity;
using SimpleWebApp.Common;
using SimpleWebApp.WebServices.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleWebApp.WebServices.Controllers
{
    [ApiAuthenticationFilter]
    [RoutePrefix("api/Authenticate")]
    public class AuthenticateController : ApiController
    {
        #region Services

        [Dependency]
        public ITokenService TokenService { get; set; }

        #endregion Services

        #region Public Constructor

        /// <summary>
        /// Public constructor 
        /// </summary>
        public AuthenticateController()
        {
            //Configuration Unity
            UnityInjector.ConfigureMe(typeof(AuthenticateController), this);
        }

        #endregion

        // <summary>
        /// Authenticates user and returns token with expiry.
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [Route("")]
        [Route("get/token")]
        [HttpPost]
        public HttpResponseMessage Authenticate()
        {
            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
            return null;
        }

        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="utilisateurId"></param>
        /// <returns></returns>
        private HttpResponseMessage GetAuthToken(int utilisateurId)
        {
            var token = TokenService.GenerateToken(utilisateurId);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings[ConfigToken.KeyForAuthTokenExpiry]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }
    }
}
