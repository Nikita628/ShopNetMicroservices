using Microsoft.EntityFrameworkCore;
using OrderModel = Order.Infrastructure.Database.Models.Order;
using ModelBase = Order.Infrastructure.Database.Models.ModelBase;

namespace Order.Infrastructure.Database
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base()
        {
        }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionString");
            }
        }

        public DbSet<OrderModel> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ModelBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "nik";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "nik";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}