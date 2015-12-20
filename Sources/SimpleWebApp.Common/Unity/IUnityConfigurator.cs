

using Microsoft.Practices.Unity;

namespace SimpleWebApp.Common.Unity
{
    /// <summary>
    /// Permet de donner la responsabilitée a la sous classe de configurer le container unity
    /// </summary>
    public interface IUnityConfigurator
    {
        void Configure(IUnityContainer container);
    }
}
