using System;
using System.Runtime.Serialization;

namespace SimpleWebApp.Common.Exceptions
{
    /// <summary>
    /// Exception
    /// L'entitée n'est pas trouvable en base
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        #region Constructors

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EntityNotFoundException(Exception innerException)
            : base("L'entitée n'est pas trouvable en base", innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
