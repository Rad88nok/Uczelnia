using APBD7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private s18361Context _context;
        private readonly IConfiguration _configuration;
        public ClientsController(s18361Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var client = _context.Clients
                                .Select(c => new
                                {
                                    Naziwsko = c.LastName
                                });
            return Ok(client);
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            var ifTMP = _context.ClientTrips.Where(c => c.IdClient==id).ToList().Exists(c => c.IdClient == id); 
            if (ifTMP) {
                var client = _context.Clients
                        .Where(c => c.IdClient == id)
                        .ToList();
                _context.Clients.Remove(client.First());
                _context.SaveChanges();

                return Ok(id + " successfully deleted");
            }
            else
            {
                return BadRequest("Client "+ id+ " is registered for the trip");
            }
        }


    }
}