using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using cdr_reset.Repository.Repositories;
using cdr_reset.Service.Services;

namespace cdr_reset.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<PrintLogsRepository>();
            container.RegisterType<PrintLogsService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}