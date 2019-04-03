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
        ShopDataDbContext context = new ShopDataDbContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> Get()
        {
            return await context.ProductCategories.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> Get(int id)
        {
            var productcategory = await context.ProductCategories.FindAsync(id);

            if (productcategory == null)
            {
                return NotFound();
            }
            return productcategory;
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> Post([FromBody] ProductCategory productcategory)
        {
            context.ProductCategories.Add(productcategory);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = productcategory.ProductCategoryId }, productcategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategory>> Delete(int id)
        {
            var productcategory = await context.ProductCategories.FindAsync(id);
            if (productcategory == null)
            {
                return NotFound();
            }
            context.ProductCategories.Remove(productcategory);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductCategory newproductcategory)
        {
            if (id != newproductcategory.ProductCategoryId)
            {
                return BadRequest();
            }
            context.Entry(newproductcategory).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
