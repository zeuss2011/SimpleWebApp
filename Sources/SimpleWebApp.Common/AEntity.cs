using System;
using System.Runtime.Serialization;

namespace SimpleWebApp.Common
{
    /// <summary>
    /// Description of the base entity
    /// </summary>
    /// <typeparam name="T">Type of primary key</typeparam>
    [DataContract(Name = "AEntity", Namespace = "Foromes", IsReference = true)]
    public abstract class AEntity<T>
    {
        /// <summary>
        /// Private id field
        /// </summary>
        private T _id = default(T);

        /// <summary>
        /// Id member
        /// </summary>
        [DataMember()]
        public virtual T Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Equality comparaison
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Boolean Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, this))
            {
                return true;
            }
            else if (obj is AEntity<T> && obj.GetType().Equals(this.GetType()))
            {
                return ((obj as AEntity<T>).Id.Equals(this.Id));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// used for list deletion in persistance
        /// </summary>
        public Boolean ShouldDelete
        {
            get;
            set;
        }
    }
}