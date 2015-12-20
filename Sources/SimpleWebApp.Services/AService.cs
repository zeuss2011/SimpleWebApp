using System;

namespace SimpleWebApp.Services
{
    public abstract class AService : IDisposable
    {
        //TODO LOGS HERE

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
