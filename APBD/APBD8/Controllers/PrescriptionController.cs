using APBD8.Models;
using APBD8.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Controllers
{
    [Route("api/prescription")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private MainDbContext _context;
        public PrescriptionController(MainDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescription(string id)
        {
            var prescription = await new PrescriptionServise().GetPrescription(_context, id);
            return Ok(prescription);
        }
    }
}
