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
        // ShopDataDbContext context = new ShopDataDbContext();
        private readonly ShopDataDbContext _context;

        public VendorController(ShopDataDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Vendor> pc = await _context.Vendors.ToListAsync();
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
            if(id==null)
            {
                return BadRequest();
            }
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }
            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    _context.Vendors.Add(vendor);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = vendor.VendorId }, vendor);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int ? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return Ok(vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] Vendor newVendor)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (id != newVendor.VendorId)
            {
                return NotFound();
            }
            _context.Entry(newVendor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}