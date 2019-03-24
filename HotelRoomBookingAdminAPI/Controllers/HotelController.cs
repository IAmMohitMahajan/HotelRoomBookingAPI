using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelRoomBookingAdminAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        DataDBContext context = new DataDBContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> Get()
        {
            return await context.Hotels.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> Get(int id)
        {
            var hotel = await context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }
            return hotel;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Hotel>>> Post([FromBody]Hotel hotel)
        {
            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = hotel.HotelId }, hotel);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id ,[FromBody]Brand newbrand)
        //{
        //    var brand = await context.Brand.FindAsync(id);

        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }
        //    brand.BrandName = newbrand.BrandName;
        //    brand.BrandDescription = newbrand.BrandDescription;

        //    await context.SaveChangesAsync();
        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Hotel hotel)
        {

            if (id != hotel.HotelId)
            {
                return BadRequest();
            }
            //brand.BrandName = newbrand.BrandName;
            //brand.BrandDescription = newbrand.BrandDescription;
            context.Entry(hotel).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> Delete(int id)
        {
            var hotel = await context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }
            else
            {
                context.Hotels.Remove(hotel);
                await context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}