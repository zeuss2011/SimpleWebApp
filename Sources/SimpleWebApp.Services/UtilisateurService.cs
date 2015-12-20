namespace SimpleWebApp.Services
{
    #region
    using Microsoft.Practices.Unity;
    using SimpleWebApp.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    #endregion

    public class UtilisateurService : AService, IUtilisateurService
    {
        #region Services

        #endregion

        #region Repositories

        [Dependency]
        public IRepository<Utilisateur> RUtilisateur { set; get; }

        [Dependency]
        public IRepository<Role> RRole { set; get; }

        #endregion

        public Utilisateur GetUtilisateurById(int userId)
        {
            if(userId > 0)
                return RUtilisateur.AsQueryable().Where(u => u.Id == userId).FirstOrDefault();
            return null;
        }

        public Utilisateur GetUtilisateurByLogin(string login)
        {
            if (!string.IsNullOrEmpty(login))
                return RUtilisateur.AsQueryable().Where(u => u.Login == login).FirstOrDefault();
            return null;
        }

        public Utilisateur GetUtilisateurByLoginAndPassWord(string username, string password)
        {
            return RUtilisateur.AsQueryableDbSet().Where(u => String.Compare(u.Login, username, StringComparison.OrdinalIgnoreCase) == 0
                                  && String.Compare(u.PassswordHash, password, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
        }

        public List<Utilisateur> GetUtilisateurs()
        {
            return RUtilisateur.AsQueryable().ToList();
        }
    }
}
