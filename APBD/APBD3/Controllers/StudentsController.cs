using APBD3.Models;
using APBD3.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace APBD3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery]string orderBy)
        {
            return Ok(_dbService.GetStudents(orderBy));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] string id)
        {
            Student st = _dbService.GetStudent(id);
            if (st != null) return Ok(st);
            else return NotFound("Nie znalezniono studenta");
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] JsonDocument student)
        {
            if (student != null)
            {
                _dbService.AddStudent(student);
                return Ok();
            }
            else return NotFound("Wrong input");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] JsonDocument student)
        {
            string tmpStudentId = student.RootElement.GetProperty("studentId").GetString();

            if (student != null)
            {
                bool wynik=_dbService.UpdateStudent(student);
                if (wynik == true)
                    return Ok($"{student} update complete");
                else
                    return BadRequest("Incomplete data");
            }

            return NotFound("Student doesnt exist");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] string id)
        {
            _dbService.DeleteStudent(id);
            
            return Ok("removed");
        }
    }
}
