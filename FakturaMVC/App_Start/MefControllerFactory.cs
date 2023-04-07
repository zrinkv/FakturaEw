using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace FakturaMVC.App_Start
{
    public class MefControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _compositionContainer;

        public MefControllerFactory(CompositionContainer compositionContainer)
        {
            _compositionContainer = compositionContainer;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            Lazy<object, object> lazy = _compositionContainer.GetExports(controllerType, null, null).SingleOrDefault();
            IController controller;
            if (lazy != null)
            {
                controller = lazy.Value as IController;
            }
            else
            {
                controller = base.GetControllerInstance(requestContext, controllerType);
                _compositionContainer.ComposeParts(controller);
            }

            return controller;
        }
    }
}