using Microsoft.EntityFrameworkCore;

namespace DemoABC.Models
{
	public class ProductosDbContext:DbContext
	{
		public ProductosDbContext(DbContextOptions<ProductosDbContext> options) : base(options)
		{ }

		public DbSet<Producto> Productos { get; set; }
	}
}
