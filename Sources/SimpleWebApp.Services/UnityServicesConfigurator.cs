using SimpleWebApp.Common;
using SimpleWebApp.Common.Unity;
using SimpleWebApp.Data;
using Microsoft.Practices.Unity;

namespace SimpleWebApp.Services
{
    public class UnityServicesConfigurator : IUnityConfigurator
    {

        public void Configure(IUnityContainer container)
        {
            //first of all, configure under layer.
            new RepositoryUnityConfigurator().Configure(container);

            //Configure Services
            container.RegisterType<IUtilisateurService, UtilisateurService>();
            container.RegisterType<IProduitService, ProduitService>();
            container.RegisterType<ITokenService, TokenService>();
        }
    }
}