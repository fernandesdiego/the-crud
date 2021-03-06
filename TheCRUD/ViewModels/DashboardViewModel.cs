using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCRUD.Models;

namespace TheCRUD.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Warehouse> Warehouses { get; set; }
    }
}
