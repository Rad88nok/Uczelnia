using APBD7.DTOs.Requests;
using APBD7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private s18361Context _context;
        private readonly IConfiguration _configuration;
        public TripsController(s18361Context context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trip = _context.Trips
                                .Select(t => new
                                {
                                    name = t.Name,
                                    dateFrom = t.DateFrom

                                }).OrderByDescending(t => t.dateFrom);
            return Ok(trip);
        }
        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(CreateClientAndTripRequestDto ccatrd)
        {
            var maxId = _context.Clients.Select(c=>c.IdClient).Last();
            var idTMP = _context.Clients.Where(c => c.Pesel == ccatrd.Pesel).Select(c => c.IdClient).ToList().First();
            if (!_context.Clients.Where(c => c.Pesel == ccatrd.Pesel).ToList().Exists(c => c.Pesel == ccatrd.Pesel))
            {
                _context.Clients.Add(new Client
                {
                    IdClient = maxId + 1,
                    FirstName = ccatrd.FirstName,
                    LastName = ccatrd.LastName,
                    Email = ccatrd.Email,
                    Pesel = ccatrd.Pesel,
                    Telephone = ccatrd.Telephone
                });
            }
            if(_context.ClientTrips.Where(ct=>ct.IdClient == idTMP).ToList() != null)
            {
                return BadRequest("The client "+ccatrd.FirstName+" has already signed up for the trip") ;
            }
            if(_context.Trips.Where(t=>t.IdTrip == ccatrd.IdTrip).ToList() == null)
            {
                return BadRequest("The trip does not exist");
            }

            return Ok("Signed up");
        }
    }
}
