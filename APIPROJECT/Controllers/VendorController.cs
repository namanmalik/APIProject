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
    public class VendorController : ControllerBase
    {
        ShopDataDbContext context = new ShopDataDbContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> Get()
        {
            return await context.Vendors.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> Get(int id)
        {
            var vendor = await context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }
            return vendor;
        }

        [HttpPost]
        public async Task<ActionResult<Vendor>> Post([FromBody] Vendor vendor)
        {
            context.Vendors.Add(vendor);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = vendor.VendorId }, vendor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendor>> Delete(int id)
        {
            var vendor = await context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            context.Vendors.Remove(vendor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Vendor newVendor)
        {
            if (id != newVendor.VendorId)
            {
                return BadRequest();
            }
            context.Entry(newVendor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}