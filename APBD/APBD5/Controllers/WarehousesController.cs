using APBD5.Models;
using APBD5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD5.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    public class WarehousesController : Controller
    {
        private readonly IDbService _dbService;
        public WarehousesController(IDbService service)
        {
            _dbService = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductWarehouse([FromBody] ProductWarehouse pW)
        {
            if (pW != null && pW.Amount >= 1)
            {
                try
                {
                    return Ok(_dbService.AddProductWarehouse(pW));
                }
                catch (Exception)
                {
                    return NotFound("Wrong input");
                }
            }
            else return NotFound("Wrong input");
        }
    }
}
