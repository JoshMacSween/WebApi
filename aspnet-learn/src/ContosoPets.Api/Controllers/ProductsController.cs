using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoPets.Api.Data;
using ContosoPets.Api.Models;

namespace ContosoPets.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ContosoPetsContext _context;

    public ProductsController(ContosoPetsContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll() =>
      _context.Products.ToList();

      // Get by ID action
      [HttpGet("{id}")]
      public async Task<ActionResult<Product>> GetById(int id)
      {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
          return NotFound();
        }
        return product;
      }

      // Post
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
          _context.Products.Add(product);
          await _context.SaveChangesAsync();
          return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

      // Put
      [HttpPut("{id}")]
      public async Task<IActionResult> Update(int id, Product product)
      {
        if (id != product.Id)
        {
          return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
      }

      // Delete
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
          return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
      }
  }
}