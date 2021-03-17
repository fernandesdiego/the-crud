using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCRUD.Interfaces;
using TheCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace TheCRUD.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProdContext context) : base(context)
        {
        }

        public async Task<Product> GeProductAndWarehouseAsync(int id)
        {
            return await _context.Products.AsNoTracking().Include(x => x.Warehouse).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAndWarehouseAsync()
        {
            return await _context.Products.AsNoTracking().Include(x => x.Warehouse).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByWarehouse(int id)
        {
            return await FindAsync(x => x.Id == id);
        }
    }
}
