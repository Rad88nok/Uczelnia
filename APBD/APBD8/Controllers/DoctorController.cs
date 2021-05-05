using APBD8.Models;
using APBD8.Services;
using APBD8.DTO.Request;
using APBD8.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private MainDbContext _dbContext;

        public DoctorController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctros()
        {
            var doctors = await new DoctorService().GetDoctors(_dbContext);
            return Ok(doctors);
        }
        [HttpPost]
        public async Task<IActionResult> PostDoctor(GetDoctorInfoRequestDto doctorInput)
        {
            await new DoctorService().PostDoctor(_dbContext, doctorInput);
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(GetDoctorInfoRequestDto doctorInput, string id)
        {
            await new DoctorService().PutDoctor(_dbContext, doctorInput, id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            await new DoctorService().DeleteDoctor(_dbContext, id);
            return NoContent();
        }

    }
}
