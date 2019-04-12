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
    public class ProductCategoryController : ControllerBase
    {
        // ShopDataDbContext context = new ShopDataDbContext();
        private readonly ShopDataDbContext _context;

        public ProductCategoryController(ShopDataDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ProductCategory> pc = await _context.ProductCategories.ToListAsync();
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
            var productcategory = await _context.ProductCategories.FindAsync(id);

            if (productcategory == null)
            {
                return NotFound();
            }
            return Ok(productcategory);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCategory productcategory)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
                try
                {
                    {
                        _context.ProductCategories.Add(productcategory);
                        await _context.SaveChangesAsync();
                        return CreatedAtAction(nameof(Get), new { id = productcategory.ProductCategoryId }, productcategory);
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var productcategory = await _context.ProductCategories.FindAsync(id);
            if (productcategory == null)
            {
                return NotFound();
            }
            _context.ProductCategories.Remove(productcategory);
            await _context.SaveChangesAsync();
            return Ok(productcategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int? id, [FromBody] ProductCategory newproductcategory)
        {
            if(id==null)
            {
                return BadRequest();
            }
            if (id != newproductcategory.ProductCategoryId)
            {
                return NotFound();
            }
            _context.Entry(newproductcategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
