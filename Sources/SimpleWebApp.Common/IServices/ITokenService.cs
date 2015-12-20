
namespace SimpleWebApp.Common
{
    public interface ITokenService
    {
        #region Interface member methods.

        /// <summary>
        ///  Function to generate unique token with expiry against the provided utilisateurId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="utilisateurId"></param>
        /// <returns></returns>
        TokenEntity GenerateToken(int utilisateurId);

        /// <summary>
        /// Function to validate token againt expiry and existance in database.
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns></returns>
        bool ValidateToken(string authToken);

        /// <summary>
        /// Method to kill the provided authToken .
        /// </summary>
        /// <param name="authToken"></param>
        bool Kill(string authToken);

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="utilisateurId"></param>
        /// <returns></returns>
        bool DeleteByUtilisateurId(int utilisateurId);

        #endregion
    }
}