using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD4.Services;
using System.Text.Json;
using APBD4.Models;

namespace APBD4.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class Animals : Controller
    {
        private readonly IdbService _dbService;
        public Animals(IdbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAnimals()
        {
            return Ok(_dbService.GetAnimals());
        }
        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string orderBy)
        {
            return Ok(_dbService.GetAnimals(orderBy));
        }
        [HttpPost]
        public async Task<IActionResult> AddAnimals([FromBody] Animal animal)
        {
            if (animal != null)
            {
                if (_dbService.AddAnimals(animal) > 0)
                    return Ok(animal);
                return Ok();
            }
            else return NotFound("Wrong input");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal([FromRoute] Animal animal, string id)
        {
            if (animal != null)
            {
                if (_dbService.UpdateAnimal(animal,id)>0)
                    return Ok($"{animal} update complete");
                else
                    return BadRequest("Incomplete data");
            }

            return NotFound("Student doesnt exist");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] string id)
        {
            if (_dbService.DeleteAnimal(id) > 0)
                return Ok("removed");
            else
                return NotFound("Not found animal");
           
        }

    }
}
