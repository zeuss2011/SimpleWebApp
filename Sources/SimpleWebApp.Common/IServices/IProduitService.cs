
using System.Collections.Generic;
namespace SimpleWebApp.Common
{
    public interface IProduitService
    {
        Produit GetProduitById(int userId);
        Produit GetProduitByNom(string nom);
        List<Produit> GetProduits();
        int CreerProduit(Produit produit);
        bool EnregistrerProduit(Produit produit);
        bool SupprimerProduit(int produitId);
    }
}
