using System;
using System.Runtime.Serialization;

namespace SimpleWebApp.Common.Exceptions
{
    /// <summary>
    /// Exception
    /// L'entitée n'est pas trouvable en base
    /// </summary>
    [Serializable]
    public class EntityUpdateFailedException : Exception
    {
        #region Constructors

        public EntityUpdateFailedException()
        {
        }

        public EntityUpdateFailedException(string message)
            : base(message)
        {
        }

        public EntityUpdateFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EntityUpdateFailedException(Exception innerException)
            : base("Une entitée n'a pas pu être mise à jour. Voir l'exception interne pour plus de détails.", innerException)
        {
        }

        protected EntityUpdateFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
