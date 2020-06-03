using MarsTerraform.Services;
using MarsTerraform.Services.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MarsTerraform.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IGameService, GameService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}