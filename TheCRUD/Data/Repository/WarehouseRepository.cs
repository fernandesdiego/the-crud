using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCRUD.Interfaces;
using TheCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace TheCRUD.Data.Repository
{
    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(ProdContext context) : base(context)
        {
        }

    }
}
