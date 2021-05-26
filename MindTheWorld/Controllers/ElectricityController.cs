﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTheWorld.Services.Definitions;
using System.Threading.Tasks;

namespace MindTheWorldServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityController : ControllerBase
    {
        private readonly IEnergyService _energyService;

        public ElectricityController(IEnergyService energyService)
        {
            _energyService = energyService;
        }

        public async Task<IActionResult> Post(IFormFile file)
        {
            return Ok(await _energyService.ImportResidentialElectricityUses(file));
        }
    }
}