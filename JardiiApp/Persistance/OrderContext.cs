using JardiiApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JardiiApp.Persistance
{
    class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        protected OrderContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Order> Orders => Set<Order>();
    }
}
