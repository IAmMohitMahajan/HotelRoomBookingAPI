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
    public class HotelTypeController : ControllerBase
    {
        DataDBContext context = new DataDBContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelType>>> Get()
        {
            return await context.HotelTypes.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<HotelType>> Get(int id)
        {
            var hotelType = await context.HotelTypes.FindAsync(id);

            if (hotelType == null)
            {
                return NotFound();
            }
            return hotelType;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<HotelType>>> Post([FromBody]HotelType hotelType)
        {
            context.HotelTypes.Add(hotelType);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = hotelType.HotelTypeId }, hotelType);
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
        public async Task<ActionResult> Put(int id, [FromBody]HotelType hotelType)
        {

            if (id != hotelType.HotelTypeId)
            {
                return BadRequest();
            }
            //brand.BrandName = newbrand.BrandName;
            //brand.BrandDescription = newbrand.BrandDescription;
            context.Entry(hotelType).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelType>> Delete(int id)
        {
            var hotelType = await context.HotelTypes.FindAsync(id);

            if (hotelType == null)
            {
                return NotFound();
            }
            else
            {
                context.HotelTypes.Remove(hotelType);
                await context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}