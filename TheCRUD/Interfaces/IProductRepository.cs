using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCRUD.Models;

namespace TheCRUD.Interfaces
{
    interface IProductRepository : IRepository<Product>
    {
        Task<Product> GeProductAndWarehouseAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAndWarehouseAsync();
        Task<IEnumerable<Product>> GetProductsByWarehouse(int id);

    }
}
