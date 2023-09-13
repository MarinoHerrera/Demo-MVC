using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class ProductosDbContext : DbContext
    {
        public ProductosDbContext(DbContextOptions<ProductosDbContext> options) : base(options)
        { }

        public DbSet<Producto> Productos { get; set; }
    }
}
