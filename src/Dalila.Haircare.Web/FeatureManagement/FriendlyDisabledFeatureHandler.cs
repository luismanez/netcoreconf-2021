using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;

namespace Dalila.Haircare.Web.FeatureManagement
{
    public class FriendlyDisabledFeatureHandler: IDisabledFeaturesHandler
    {
        public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
        {
            context.Result = new OkObjectResult("Hey, this Feature is disable for you. Don´t take it personal!");
            return Task.CompletedTask;
        }
    }
}