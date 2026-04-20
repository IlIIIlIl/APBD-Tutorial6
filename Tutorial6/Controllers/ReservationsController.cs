using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorial6.DTOs;
using Tutorial6.Models;

namespace Tutorial6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {

        public static List<Reservation> Reservations = new List<Reservation>()
        {
            new Reservation()
            {
                Id = 1,
                RoomId = 1,
                OrganizerName = "Bob",
                Topic = "Calculus",
                Date = "2026-04-04",
                StartTime = "10:00:00",
                EndTime = "11:30:00",
                Status = "Confirmed"
            },
            new Reservation()
            {
                Id = 2,
                RoomId = 1,
                OrganizerName = "John",
                Topic = "JavaScript",
                Date = "2026-04-04",
                StartTime = "11:45:00",
                EndTime = "18:00:00",
                Status = "Planned"
            },
            new Reservation()
            {
                Id = 3,
                RoomId = 2,
                OrganizerName = "John",
                Topic = "Fortune telling",
                Date = "2026-01-04",
                StartTime = "8:45:00",
                EndTime = "10:45:00",
                Status = "Confirmed"
            },
            new Reservation()
            {
                Id = 4,
                RoomId = 4,
                OrganizerName = "Cthulhu",
                Topic = "The king in yellow",
                Date = "2026-07-07",
                StartTime = "00:00:00",
                EndTime = "12:00:00",
                Status = "Cancelled"
            }
        };
        
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromRoute] int id)
        {
            var reservation = Reservations.FirstOrDefault(a => a.Id == id);
            
            if (reservation == null)
            {
                return NotFound();
            }
            
            return Ok(reservation);
        }
        
        [HttpGet]
        public IActionResult GetReservations(
            [FromQuery] string? status,
            [FromQuery] string? endTime, 
            [FromQuery] string? startTime, 
            [FromQuery] string? date, 
            [FromQuery] string? topic, 
            [FromQuery] string? organizerName, 
            [FromQuery] int? roomId,  
            [FromQuery] int? id)
        {
            var query = Reservations.AsQueryable();

            if (id.HasValue) query = query.Where(a => a.Id == id);
            if (roomId.HasValue) query = query.Where(a => a.RoomId == roomId);
            if (!string.IsNullOrEmpty(organizerName)) query = query.Where(a => a.OrganizerName.Contains(organizerName, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(topic)) query = query.Where(a => a.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(date)) query = query.Where(a => a.Date.Contains(date, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(startTime)) query = query.Where(a => a.StartTime.Equals(startTime, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(endTime)) query = query.Where(a => a.EndTime.Contains(endTime, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(status)) query = query.Where(a => a.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            return Ok(query.ToList());
        }
        
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateReservationDto createReservationDto)
        {
            var reservation = new Reservation()
            {
                Id = Reservations.Count + 1,
                RoomId = createReservationDto.RoomId,
                OrganizerName = createReservationDto.OrganizerName,
                Topic = createReservationDto.Topic,
                Date = createReservationDto.Date,
                StartTime = createReservationDto.StartTime,
                EndTime = createReservationDto.EndTime,
                Status = createReservationDto.Status
            };
            
            Reservations.Add(reservation);
            
            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
            
        }
        
        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var reservation = Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }
            
            Reservations.Remove(reservation);
            
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] CreateReservationDto createReservationDto)
        {
            var reservation = Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }
            
            reservation.RoomId = createReservationDto.RoomId;
            reservation.OrganizerName = createReservationDto.OrganizerName;
            reservation.Topic = createReservationDto.Topic;
            reservation.Date = createReservationDto.Date;
            reservation.StartTime = createReservationDto.StartTime;
            reservation.EndTime = createReservationDto.EndTime;
            reservation.Status = createReservationDto.Status;
            
            return NoContent();
        }
        

    }
}
