using System.Collections.Generic;

namespace Dalila.Haircare.Web.FeatureManagement.DepartmentFilter
{
    public class DepartmentFilterSettings
    {
        public IList<string> AllowedDepartments { get; set; } = new List<string>();
    }
}