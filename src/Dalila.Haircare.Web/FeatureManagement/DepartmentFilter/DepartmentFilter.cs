using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Microsoft.Graph;

namespace Dalila.Haircare.Web.FeatureManagement.DepartmentFilter
{
    [FilterAlias(Common.Constants.Features.DepartmentFilterAlias)]
    public class DepartmentFilter: IFeatureFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            // Note: FeatureFilters are registered as Singleton, but GraphServiceClient is registered as Scoped
            // so here we are creating a Scope to be able to retrieve the GraphServiceClient and call Graph API.
            // Pattern taken from here: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio#consuming-a-scoped-service-in-a-background-task
            // However, for Production systems, I suggest you to extend Claims with the Department, and here use the IHttpContextAccessor
            using var scope = _serviceProvider.CreateScope();
            
            var graphServiceClient =
                scope.ServiceProvider
                    .GetRequiredService<GraphServiceClient>();

            var me = await graphServiceClient.Me
                .Request()
                .Select(user => user.Department)
                .GetAsync();

            var meDepartment = me.Department;

            var settings = context.Parameters.Get<DepartmentFilterSettings>() ?? new DepartmentFilterSettings();

            return settings.AllowedDepartments.Contains(meDepartment, StringComparer.OrdinalIgnoreCase);
        }
    }
}