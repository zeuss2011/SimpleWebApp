using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleWebApp.Common
{
    public interface IRepository<T> : IDisposable where T : AEntity<int>
    {
        /// <summary>
        /// Enable or not the lazy loading
        /// </summary>
        /// <param name="active"></param>
        void EnableLazyLoading(bool active);


        /// <summary>
        /// Gets the element having the specified identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Permet de query la table
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AsQueryable();

        /// <summary>
        /// A manipuler avec précaution, sert à permettre le Eager Loading au niveau des services (via Include)
        /// </summary>
        /// <returns></returns>
        DbSet<T> AsQueryableDbSet();

        /// <summary>
        /// Recupere tous les eelements
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Recupere un element suivant un predicat
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Single(int id);

        /// <summary>
        /// Recupere un element de manière asynchrone en suivant un predicat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> SingleAsync(int id);

        /// <summary>
        /// Mettre à jour un élément ayant été modifié.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        T InsertOrUpdate(T entityToUpdate);

        /// <summary>
        /// Supprimer de la base de données l'objet passé en paramètre.
        /// </summary>
        /// <param name="entityToDel"></param>
        void Delete(T entityToDel);

        /// <summary>
        /// Enregistrer toutes les modifications ayant été réalisées dans le context courant.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Annuler l'ensemble des modifications opérées dans le context actuel.
        /// </summary>
        void RevertChanges();

        /// <summary>
        /// Indexer sur l'id
        /// </summary>
        /// <param name="Id">id de l'objet à trouver</param>
        /// <returns>l'Objet typé</returns>
        T this[int id] { get; }
    }
}
