namespace SimpleWebApp.Services
{
    #region
    using Microsoft.Practices.Unity;
    using SimpleWebApp.Common;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Mail;
    #endregion

    public class TokenService : AService, ITokenService
    {
        #region Services

        #endregion Services

        #region Repositories

        [Dependency]
        public IRepository<TokenEntity> RTokenEntity { set; get; }

        #endregion

        /// <summary>
        ///  Function to generate unique token with expiry against the provided utilisateurId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="utilisateurId"></param>
        /// <returns></returns>
        public TokenEntity GenerateToken(int utilisateurId)
        {
            string authToken = Guid.NewGuid().ToString();
            DateTime dateCreation = DateTime.Now;
            DateTime dateExpiration = DateTime.Now.AddSeconds(
            Convert.ToDouble(ConfigurationManager.AppSettings[ConfigToken.KeyForAuthTokenExpiry]));
            var token = new TokenEntity
            {
                UtilisateurId = utilisateurId,
                AuthToken = authToken,
                DateCreation = dateCreation,
                DateExpiration = dateExpiration
            };

            SaveToken(token);

            return token;
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public bool ValidateToken(string authToken)
        {
            var token = RTokenEntity.AsQueryable().Where(t => t.AuthToken == authToken && t.DateExpiration > DateTime.Now).FirstOrDefault();
            if (token != null && !(DateTime.Now > token.DateExpiration))
            {
                token.DateExpiration = token.DateExpiration.AddSeconds(
                Convert.ToDouble(ConfigurationManager.AppSettings[ConfigToken.KeyForAuthTokenExpiry]));
                SaveToken(token);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="authToken">true for successful delete</param>
        public bool Kill(string authToken)
        {
            RTokenEntity.AsQueryable().Where(t => t.AuthToken == authToken).ToList().ForEach(RTokenEntity.Delete);
            RTokenEntity.SaveChanges();
            return !RTokenEntity.AsQueryable().Any(t => t.AuthToken == authToken);
        }

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="utilisateurId"></param>
        /// <returns>true for successful delete</returns>
        public bool DeleteByUtilisateurId(int utilisateurId)
        {
            RTokenEntity.AsQueryable().Where(t => t.UtilisateurId == utilisateurId).ToList().ForEach(RTokenEntity.Delete);
            RTokenEntity.SaveChanges();
            return !RTokenEntity.AsQueryable().Any(t => t.UtilisateurId == utilisateurId);
        }

        private void SaveToken(TokenEntity tokenModel)
        {
            RTokenEntity.InsertOrUpdate(tokenModel);
            RTokenEntity.SaveChanges();
        }
    }
}
