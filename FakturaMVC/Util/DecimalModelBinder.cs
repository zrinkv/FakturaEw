using System;
using System.Web.Mvc;

namespace FakturaMVC
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);decimal temp = decimal.Parse(valueProviderResult.AttemptedValue.Replace(".", ","));

            return valueProviderResult == null ? base.BindModel(controllerContext, bindingContext) : Convert.ToDecimal(valueProviderResult.AttemptedValue.Replace(".", ","));
        }
    }
}