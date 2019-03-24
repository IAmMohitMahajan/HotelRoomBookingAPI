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
    public class RoomFacilityController : ControllerBase
    {
        DataDBContext context = new DataDBContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomFacility>>> Get()
        {
            return await context.RoomFacilities.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RoomFacility>> Get(int id)
        {
            var roomFacility = await context.RoomFacilities.FindAsync(id);

            if (roomFacility == null)
            {
                return NotFound();
            }
            return roomFacility;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<RoomFacility>>> Post([FromBody]RoomFacility roomFacility)
        {
            context.RoomFacilities.Add(roomFacility);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = roomFacility.RoomFacilityId }, roomFacility);
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
        public async Task<ActionResult> Put(int id, [FromBody]RoomFacility roomFacility)
        {

            if (id != roomFacility.RoomFacilityId)
            {
                return BadRequest();
            }
            //brand.BrandName = newbrand.BrandName;
            //brand.BrandDescription = newbrand.BrandDescription;
            context.Entry(roomFacility).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RoomFacility>> Delete(int id)
        {
            var roomFacility = await context.RoomFacilities.FindAsync(id);

            if (roomFacility == null)
            {
                return NotFound();
            }
            else
            {
                context.RoomFacilities.Remove(roomFacility);
                await context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}