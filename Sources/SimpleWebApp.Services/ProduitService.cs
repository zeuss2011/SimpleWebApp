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

    public class ProduitService : AService, IProduitService
    {
        #region Services

        #endregion

        #region Repositories

        [Dependency]
        public IRepository<Produit> RProduit { set; get; }

        #endregion

        public Produit GetProduitById(int produitId)
        {
            if(produitId > 0)
                return RProduit.AsQueryable().Where(p => p.Id == produitId).FirstOrDefault();
            return null;
        }

        public Produit GetProduitByNom(string nom)
        {
            if (!string.IsNullOrEmpty(nom))
                return RProduit.AsQueryable().Where(u => u.Nom == nom).FirstOrDefault();
            return null;
        }

        public List<Produit> GetProduits()
        {
            return RProduit.AsQueryable().ToList();
        }

        public int CreerProduit(Produit produit)
        {
            if(produit != null && produit.Id == 0)
            {
                RProduit.InsertOrUpdate(produit);
                RProduit.SaveChanges();
                return produit.Id;
            }
            return -1;
        }

        public bool EnregistrerProduit(Produit produit)
        {
            if (produit != null && produit.Id > 0 && RProduit.AsQueryable().Any(p => p.Id == produit.Id))
            {
                RProduit.InsertOrUpdate(produit);
                RProduit.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SupprimerProduit(int produitId)
        {
            if (produitId > 0)
            {
                var produit = RProduit.AsQueryable().FirstOrDefault(p => p.Id == produitId);
                if(produit != null)
                {
                    RProduit.Delete(produit);
                    RProduit.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
