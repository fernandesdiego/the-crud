using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheCRUD.Models;

namespace TheCRUD.Data
{
    public class ProdContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public ProdContext(DbContextOptions<ProdContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is Product && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

            foreach (var entityEntry in entries)
            {
                ((Product)entityEntry.Entity).LastUpdated = DateTime.Now;
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is Product && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

            foreach (var entityEntry in entries)
            {
                ((Product)entityEntry.Entity).LastUpdated = DateTime.Now;
            }

            return base.SaveChangesAsync(true, cancellationToken);
        }

    }
}
