using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web.Mvc;

namespace FakturaMVC.App_Start
{
    public static class MefConfig
    {
        public static void RegisterMef()
        {
            var container = ConfigureContainer();

            ControllerBuilder.Current.SetControllerFactory(new MefControllerFactory(container));

            var dependencyResolver = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver;
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new MefDependencyResolver(container);
        }

        private static CompositionContainer ConfigureContainer()
        {
            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var businessRulesCatalog = new AssemblyCatalog(typeof(FakturaMEF.IPorezMetaData).Assembly);
            var catalogs = new AggregateCatalog(assemblyCatalog, businessRulesCatalog);
            var container = new CompositionContainer(catalogs);
            return container;
        }
    }
}