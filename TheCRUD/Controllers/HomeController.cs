using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheCRUD.Data;
using TheCRUD.Models;
using TheCRUD.ViewModels;
using TheCRUD.Interfaces;

namespace TheCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProdContext _context;
        private readonly IWarehouseRepository _warehouseContext;
        private readonly IProductRepository _productContext;

        public HomeController(ILogger<HomeController> logger, ProdContext context, IWarehouseRepository warehouseContext, IProductRepository productContext)
        {
            _logger = logger;
            _context = context;
            _warehouseContext = warehouseContext;
            _productContext = productContext;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<JsonResult> DashboardData()
        {
            var warehouses = await _warehouseContext.GetAllWarehousesAndProductsAsync();
            //var warehouses = _context
            //    .Warehouses
            //    .Include(x => x.Products)
            //    .ToList();

            //var products = _context
            //    .Products
            //    .Include(x => x.Warehouse)
            //    .ToList();

            DashboardViewModel viewModel = new DashboardViewModel() { Warehouses = warehouses};
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return Json(JsonSerializer.Serialize(viewModel, options));
        }
        public IActionResult Privacy()
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
