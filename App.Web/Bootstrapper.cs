using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using App.Web.Controllers;
using App.Core.Services;
using App.Web.Code.Unity;
using App.Web.Code.Attribute;
using System.Net.Mail;
using System.Linq;
using System.Web.Http;

namespace App.Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            /// Filter Injection
            var oldProvider = FilterProviders.Providers.Single(
                 f => f is FilterAttributeFilterProvider
             );
            FilterProviders.Providers.Remove(oldProvider);

            var container = new UnityContainer();
            var provider = new UnityFilterAttributeFilterProvider(container);
            FilterProviders.Providers.Add(provider);
            /// End Filter Injection

            //container.RegisterType<MembershipAuthorize>(new InjectionMember[]
            //{
            //    new InjectionProperty("userService", new ResolvedParameter<IUserService>())
            //});

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();        

            container.RegisterInstance<IUnityContainer>(container);

            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IController, CategoryController>("Category");
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IConfigService, ConfigService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<SmtpClient>(new InjectionConstructor());

            //ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
            return container;

        }
    }
}