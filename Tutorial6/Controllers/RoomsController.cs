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
                Name = "BigClassRoom2", 
                Floor = 2, 
                BuildingCode = "23a", 
                Capacity = 6, 
                HasProjector = false, 
                IsActive = true},
            new Room() {
                Id = 4, 
                Name = "VeryBigClassRoom1", 
                Floor = 300, 
                BuildingCode = "23a", 
                Capacity = 2, 
                HasProjector = false, 
                IsActive = false}
        };

        
        
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromRoute] int id)
        {
            var room = Rooms.FirstOrDefault(a => a.Id == id);
            
            if (room == null)
            {
                return NotFound();
            }
            
            return Ok(room);
        }
        
        [Route("building/{code}")]
        [HttpGet]
        public IActionResult GetByBuildingCode([FromRoute] string code)
        {
            var rooms = Rooms.Where(a => a.BuildingCode == code);
            
            if (!rooms.Any())
            {
                return NotFound();
            }
            
            return Ok(rooms);
        }
        
        [HttpGet]
        public IActionResult GetRooms(
            [FromQuery] int? floor, 
            [FromQuery] string? name, 
            [FromQuery] string? buildingCode, 
            [FromQuery] bool? hasProjector, 
            [FromQuery] bool? isActive, 
            [FromQuery] int? capacity,  
            [FromQuery] int? id)
        {
            var query = Rooms.AsQueryable();

            if (id.HasValue) query = query.Where(a => a.Id == id);
            if (floor.HasValue) query = query.Where(a => a.Floor >= floor);
            if (capacity.HasValue) query = query.Where(a => a.Capacity >= capacity);
            if (!string.IsNullOrEmpty(name)) query = query.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(buildingCode)) query = query.Where(a => a.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase));
            if (hasProjector.HasValue) query = query.Where(a => a.HasProjector == hasProjector);
            if (isActive.HasValue) query = query.Where(a => a.IsActive == isActive);

            return Ok(query.ToList());
        }
        
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateRoomDto createRoomDto)
        {
            var room = new Room()
            {
                Id = Rooms.Count + 1,
                Name = createRoomDto.Name,
                BuildingCode = createRoomDto.BuildingCode,
                Capacity = createRoomDto.Capacity,
                HasProjector = createRoomDto.HasProjector,
                IsActive = createRoomDto.IsActive,
                Floor = createRoomDto.Floor,
            };
            
            Rooms.Add(room);
            
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
            
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound($"Room with ID {id} was not found.");
            }
            
            Rooms.Remove(room);
            
            return NoContent();
        }

    }
}
