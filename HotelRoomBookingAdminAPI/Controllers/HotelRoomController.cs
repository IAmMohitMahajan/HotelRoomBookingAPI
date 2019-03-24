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
    public class HotelRoomController : ControllerBase
    {
        DataDBContext context = new DataDBContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> Get()
        {
            return await context.HotelRooms.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<HotelRoom>> Get(int id)
        {
            var hotelRoom = await context.HotelRooms.FindAsync(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }
            return hotelRoom;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> Post([FromBody]HotelRoom hotelRoom)
        {
            context.HotelRooms.Add(hotelRoom);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = hotelRoom.RoomId }, hotelRoom);
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
        public async Task<ActionResult> Put(int id, [FromBody]HotelRoom hotelRoom)
        {

            if (id != hotelRoom.RoomId)
            {
                return BadRequest();
            }
            //brand.BrandName = newbrand.BrandName;
            //brand.BrandDescription = newbrand.BrandDescription;
            context.Entry(hotelRoom).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelRoom>> Delete(int id)
        {
            var hotelRoom = await context.HotelRooms.FindAsync(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }
            else
            {
                context.HotelRooms.Remove(hotelRoom);
                await context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}