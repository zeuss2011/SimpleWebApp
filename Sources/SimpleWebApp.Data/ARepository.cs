using SimpleWebApp.Common;
using SimpleWebApp.Common.Exceptions;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Data
{


    /// <summary>
    /// Expose les fonctionnalités de lecture d'un Repository.
    /// </summary>
    /// <typeparam name="T">Type of entity tracked</typeparam>
    /// <typeparam name="TK">Type of primary key</typeparam>
    public abstract class ARepository<T, TK> where T : AEntity<TK>
    {
        protected static readonly ILog Logger = LogManager.GetLogger("SimpleWebApp.Data");

        #region Constructor(s)

        protected ARepository(DbContext context)
        {
            if (context == null)
                Logger.Error("Erreur lors de la creation du context");

            Context = context;
            if (Context != null) Context.Configuration.LazyLoadingEnabled = false;
            HasSharedContext = true;
        }

        ~ARepository()
        {
            Dispose();
        }

        #endregion

        #region Methods

        public void EnableLazyLoading(bool active)
        {
            Context.Configuration.LazyLoadingEnabled = active;
        }

        /// <summary>
        /// Gets the element having the specified identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract T GetById(TK id);

        public IQueryable<T> AsQueryable()
        {
            return Entity;
        }

        /// <summary>
        /// A manipuler avec précaution, sert à permettre le Eager Loading dans la couche service
        /// </summary>
        /// <returns></returns>
        public DbSet<T> AsQueryableDbSet()
        {
            return Entity;
        }

        public virtual List<T> GetAll()
        {
            return Entity.ToList();
        }

        public virtual T Single(TK id)
        {
            try
            {
                var result = Entity.Find(id);
                if (null == result)
                    throw new EntityNotFoundException();
                return result;
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public async virtual Task<T> SingleAsync(TK id)
        {
            try
            {
                var result = await Entity.FindAsync(id);
                if (null == result)
                    throw new EntityNotFoundException();
                return result;
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        /// <summary>
        /// Ajouter un nouvel objet dans la base de données.
        /// </summary>
        /// <param name="entityToAdd"></param>
        private void Insert(T entityToAdd)
        {
            if (entityToAdd == null)
                throw new InvalidOperationException("entityToAdd cannot be null");

            if (!entityToAdd.Id.Equals(default(TK)))
                throw new InvalidOperationException("entityToUpdate must not have an identifier");

            Entity.Add(entityToAdd);
        }

        /// <summary>
        /// Mettre à jour un élément ayant été modifié.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual T InsertOrUpdate(T entityToUpdate)
        {
            if (entityToUpdate == null)
                throw new InvalidOperationException("entity cannot be null");

            try
            {
                //id non défini => Insertion
                if (entityToUpdate.Id.Equals(default(TK)))
                {
                    return InsertEntity(entityToUpdate);
                }

                var realEntity = Single(entityToUpdate.Id);
                Map(entityToUpdate, realEntity);
                Entity.AddOrUpdate(realEntity);
                return entityToUpdate;
            }
            catch (EntityNotFoundException) //l'entite n'a pas été trouvé
            {
                return InsertEntity(entityToUpdate);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw new EntityUpdateFailedException(e);
            }
        }

        private T InsertEntity(T entity)
        {
            this.Entity.Add(entity); //a default de pouvoir la maj, on l'insert
            return entity;
        }

        /// <summary>
        /// Supprimer de la base de données l'objet passé en paramètre.
        /// </summary>
        /// <param name="entityToDel"></param>
        public virtual void Delete(T entityToDel)
        {
            if (entityToDel == null)
            {
                throw new InvalidOperationException("entityToDel cannot be null");
            }

            if (entityToDel.Id.Equals(default(TK)))
            {
                throw new InvalidOperationException("entityToDel must have an identifier");
            }

            T existingEntity = Entity.Find(entityToDel.Id);
            if (existingEntity != null)
            {
                Entity.Remove(existingEntity);
            }
            else
            {
                throw new InvalidOperationException("entityToDel doesn't exist in the database");
            }
        }

        /// <summary>
        /// Map les proprietes de la source vers le target, si elles sont differentes
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        protected abstract void Map(T source, T target);

        /// <summary>
        /// Enregistrer toutes les modifications ayant été réalisées dans le context courant.
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                if (Context.GetValidationErrors().Count() <= 0)
                {
                    Context.SaveChanges();
                }
                else
                {
                    Context.ChangeTracker.DetectChanges();
                    Context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //je log le détail des erreurs de validation
                //pour pouvoir retrouver ou la validation échoue, c'est necessaire
                //en debug s'il break, il ne donne pas ces détails
                //et pourtant il est beaucoup plus simple de trouver d'ou vient l'erreur de validation
                //avec cette liste de message
                Logger.Error("Erreur lors de la sauvegarde en base de donnée: ", e);

                string textError = e.Message;
                if (Context.GetValidationErrors().Count() > 0)
                {
                    Logger.Info("([" + typeof(T).ToString() + "] Messages de validation: ");

                    textError = "Validation errors : ";
                    foreach (var err in Context.GetValidationErrors())
                    {
                        foreach (var verr in err.ValidationErrors)
                        {
                            string msg = verr.PropertyName + ": " + verr.ErrorMessage;
                            textError += Environment.NewLine + msg;
                            Logger.Info(msg);
                        }
                    }
                }

                throw new Exception(textError, e);
            }
        }

        /// <summary>
        /// Annuler l'ensemble des modifications opérées dans le context actuel.
        /// </summary>
        public void RevertChanges()
        {
            ObjectContext dbContext = (Context as IObjectContextAdapter).ObjectContext;
            if (dbContext != null)
            {
                // delete added objects that did not get saved
                foreach (var entry in dbContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
                {
                    if (entry.Entity != null)
                        dbContext.DeleteObject(entry.Entity);
                }
                // Refetch modified objects from database
                foreach (var entry in dbContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified))
                {
                    if (entry.Entity != null)
                        dbContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, entry.Entity);
                }
                // Recover modified objects that got deleted
                foreach (var entry in dbContext.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted))
                {
                    if (entry.Entity != null)
                        dbContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, entry.Entity);
                }
            }
        }

        #endregion

        #region Properties

        // TODO Cacher l'acces a cette propriété dans les couches hautes //protected internal normalement
        public DbContext Context
        {
            get;
            set;
        }

        protected internal Boolean HasSharedContext
        {
            get;
            set;
        }

        // TODO Cacher l'acces a cette propriété dans les couches hautes
        protected internal DbSet<T> Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Context.Set<T>();
                }
                return _entity;
            }
        }

        /// <summary>
        /// Indexer sur l'id
        /// </summary>
        /// <param name="Id">id de l'objet à trouver</param>
        /// <returns>l'Objet typé</returns>
        public T this[TK id]
        {
            get
            {
                return GetById(id);
            }
        }

        #endregion

        #region Fields

        private DbSet<T> _entity;

        #endregion


        #region IDisposable Impl.

        protected Boolean Disposed
        {
            get;
            private set;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                    Context = null;
                }
                Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
