using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace Dalila.Haircare.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesController: ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        public FeaturesController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var featuresList = new Dictionary<string, bool>();
            var features = _featureManager.GetFeatureNamesAsync();
            
            await foreach (var feature in features)
            {
                var isEnabled = await _featureManager.IsEnabledAsync(feature);
                featuresList.Add(feature, isEnabled);
            }

            return Ok(featuresList);
        }
    }
}