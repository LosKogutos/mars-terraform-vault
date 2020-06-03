using MarsTerraform.Controllers;
using MarsTerraform.Models;
using MarsTerraform.Services;
using MarsTerraform.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace MarsTerraform.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();

            container.RegisterType<IGameService, GameService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}