
using System.Collections.Generic;
namespace SimpleWebApp.Common
{
    public interface IUtilisateurService
    {
        Utilisateur GetUtilisateurById(int userId);
        Utilisateur GetUtilisateurByLogin(string login);
        Utilisateur GetUtilisateurByLoginAndPassWord(string username, string password);
        List<Utilisateur> GetUtilisateurs();
    }
}
