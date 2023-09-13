using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
        private readonly ProductosDbContext _context;

        public ProductoController(ProductosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }


        // GET: Productoes/AddOrEdit
        [HttpGet]
        [Route("AddOrEdit")]
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return Ok(new Producto());
            else
            {
                var productos = _context.Productos.Find(id);
                return Ok(productos);
            }
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("AddOrEdit")]
        public async Task<IActionResult> AddOrEdit(Producto producto)
        {
            List<Producto> productos = new List<Producto>();

            if (ModelState.IsValid)
            {
                if (producto.Id == 0)
                {
                    _context.Add(producto);
                }
                else
                    _context.Update(producto);

                await _context.SaveChangesAsync();
            }

             productos = await _context.Productos.ToListAsync();

            return Ok(productos);
        }

       
        // POST: Productoes/Delete/5
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            List<Producto> productos = new List<Producto>();
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }
    }
}
