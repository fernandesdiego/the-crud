using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheCRUD.Data;
using TheCRUD.Interfaces;
using TheCRUD.Models;

namespace TheCRUD.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly ProdContext _context;
        private readonly IWarehouseRepository _warehouseContext;

        public WarehouseController(ProdContext context, IWarehouseRepository warehouseContext)
        {
            _context = context;
            _warehouseContext = warehouseContext;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
            return View(await _warehouseContext.GetAllAsync());
            //return View(await _context.Warehouses.ToListAsync());
        }

        // GET: Warehouse/Details/5
        public async Task<IActionResult> Details(int id)
        {           
            var warehouse = await _warehouseContext.GetByIdAsync(id);
            //var warehouse = await _context.Warehouses
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // GET: Warehouse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                await _warehouseContext.AddAsync(warehouse);
                //_context.Add(warehouse);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouse/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //var warehouse = await _context.Warehouses.FindAsync(id);
            var warehouse = await _warehouseContext.GetByIdAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouse/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _warehouseContext.UpdateAsync(warehouse);
                    //_context.Update(warehouse);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await WarehouseExists(warehouse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return View(new ErrorViewModel());
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouse/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _warehouseContext.GetByIdAsync(id);
            //var warehouse = await _context.Warehouses
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _warehouseContext.RemoveAsync(id);
            //var warehouse = await _context.Warehouses.FindAsync(id);
            //_context.Warehouses.Remove(warehouse);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WarehouseExists(int id)
        {
            var prod = await _warehouseContext.GetByIdAsync(id);

            return prod != null;
            //return _context.Warehouses.Any(e => e.Id == id);
        }
    }
}
