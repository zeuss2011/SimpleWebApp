using Microsoft.Practices.Unity;
using SimpleWebApp.Common;
using SimpleWebApp.Common.Unity;
using SimpleWebApp.Services;

namespace SimpleWebApp.WebServices
{
    public class UnityWebServicesConfigurator : IUnityConfigurator
    {

        public void Configure(IUnityContainer container)
        {
            //first of all, configure under layer.
            new UnityServicesConfigurator().Configure(container);

            //Configure Services
            //container.RegisterType<IUtilisateurService, UtilisateurService>();
            //container.RegisterType<IProduitService, ProduitService>();
        }
    }
}