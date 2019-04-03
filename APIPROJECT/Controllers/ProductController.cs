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
        ShopDataDbContext context = new ShopDataDbContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await context.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var pro = await context.Products.FindAsync(id);
            if (pro == null)
            {
                return NotFound();
            }
            return pro;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody]Product pro)
        {
            context.Products.Add(pro);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = pro.ProductId }, pro);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var pro = await context.Products.FindAsync(id);
            if (pro == null)
            {
                return NotFound();
            }
      
                context.Products.Remove(pro);
                await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Product newpro)
        {

            if (id != newpro.ProductId)
            {
                return BadRequest();
            }

            context.Entry(newpro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
    
