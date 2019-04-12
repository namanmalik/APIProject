using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPROJECT.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace APIPROJECT.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       
        private readonly ShopDataDbContext _context;

        public ProductController(ShopDataDbContext context)
        {
            _context = context;
        }
        //ShopDataDbContext context = new ShopDataDbContext();
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Product> pc = await _context.Products.ToListAsync();
            if (pc != null)
            {
                return Ok(pc);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var pro = await _context.Products.FindAsync(id);
            if (pro == null)
            {
                return NotFound();
            }
            return Ok(pro);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product pro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    _context.Products.Add(pro);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = pro.ProductId }, pro);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var pro = await _context.Products.FindAsync(id);
            if (pro == null)
            {
                return NotFound();
            }
      
                _context.Products.Remove(pro);
                await _context.SaveChangesAsync();
            return Ok(pro);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int? id, [FromBody]Product newpro)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (id != newpro.ProductId)
            {
                return NotFound();
            }

            _context.Entry(newpro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
    
