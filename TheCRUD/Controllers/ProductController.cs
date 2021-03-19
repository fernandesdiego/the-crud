using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheCRUD.Data;
using TheCRUD.Interfaces;
using TheCRUD.Models;
using TheCRUD.ViewModels;

namespace TheCRUD.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository _productContext;
        private readonly IWarehouseRepository _warehouseContext;

        public ProductController(IProductRepository productContext, IWarehouseRepository warehouseContext)
        {
            _productContext = productContext;
            _warehouseContext = warehouseContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productContext.GetAllProductsAndWarehouseAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productContext.GeProductAndWarehouseAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ProductViewModel vm = new ProductViewModel() { Warehouses = await _warehouseContext.GetAllAsync() };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                await _productContext.AddAsync(viewmodel.Product);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productContext.GeProductAndWarehouseAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productContext.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productContext.GetByIdAsync(id);            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {            
            await _productContext.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            var prod = await _productContext.GetByIdAsync(id);
            
            return prod != null;
        }
    }
}
