using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Http.Dependencies;

namespace FakturaMVC.App_Start
{
    public class MefDependencyResolver : IDependencyResolver, IDependencyScope, IDisposable
    {
        private readonly CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _container.GetExports(serviceType, null, null).SingleOrDefault()?.Value;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<Lazy<object, object>> exports = _container.GetExports(serviceType, null, null);
            List<object> list = new List<object>();
            if (exports.Any())
            {
                foreach (Lazy<object, object> item in exports)
                {
                    list.Add(item.Value);
                }

                return list;
            }

            return list;
        }

        public void Dispose()
        {
        }
    }
}