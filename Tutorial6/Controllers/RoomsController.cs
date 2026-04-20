using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorial6.DTOs;
using Tutorial6.Models;

namespace Tutorial6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {

        public static List<Room> Rooms = new List<Room>()
        {
            new Room() {
                Id = 1, 
                Name = "StandardClassRoom1", 
                Floor = 1, 
                BuildingCode = "23", 
                Capacity = 20, 
                HasProjector = true, 
                IsActive = true},
            new Room() {
                Id = 2, 
                Name = "BigClassRoom1", 
                Floor = 1, 
                BuildingCode = "23", 
                Capacity = 50, 
                HasProjector = true, 
                IsActive = true},
            new Room() {
                Id = 3, 
                Name = "BigClassRoom1", 
                Floor = 2, 
                BuildingCode = "23a", 
                Capacity = 6, 
                HasProjector = false, 
                IsActive = true}
            
        };

        [HttpGet]
        public IActionResult Get([FromQuery] int? floor = 1)
        {
            return Ok(Rooms.Where(a => a.Floor >= floor));
        }

    }
}
