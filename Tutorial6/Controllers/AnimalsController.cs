using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorial6.DTOs;
using Tutorial6.Models;

namespace Tutorial6.Controllers
{
    

// api/[controller] will accept api/animals
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase

    {
        public static List<Animal> animals = new List<Animal>()
        {
            new Animal() { Id = 1, Name = "Dog", Age = 3 },
            new Animal() { Id = 2, Name = "Cat", Age = 2 },
            new Animal() { Id = 3, Name = "Fish", Age = 4 }
        };
        
        // GET api/animals
        [HttpGet]
        public IActionResult Get([FromQuery] int? age = 0)
        {
            //.Where can be chained (like add .Where(smthsmth).Where on a new line), or || or && inside 
            return Ok(animals.Where(a => a.Age >= age));
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromRoute] int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            
            if (animal == null)
            {
                return NotFound();
            }
            
            return Ok(animal);
        }
        
        // POST api/animals { "name": "Cow", "age": 7}
        [HttpPost]
        public IActionResult Post([FromBody] CreateAnimalDto createAnimalDto)
        {
            var animal = new Animal()
            {
                Id = animals.Count + 1,
                Name = createAnimalDto.Name,
                Age = createAnimalDto.Age
            };
            
            animals.Add(animal);
            
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
            
        }
    }
}
