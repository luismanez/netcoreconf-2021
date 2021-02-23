using Dalila.Haircare.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dalila.Haircare.Web.Common;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Dalila.Haircare.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManager _featureManager;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public async Task<IActionResult> Index()
        {
            var productsListHeaderMessage = 
                await _featureManager.IsEnabledAsync(Constants.Features.ProductsListHeader)
                ? "Our Awesome Products"
                : "Our Products";

            ViewBag.ProductListHeaderMessage = productsListHeaderMessage;

            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [FeatureGate(Constants.Features.ContactPage)]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
