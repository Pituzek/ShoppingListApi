using ElectricityProviderApi.Models;
using ElectricityProviderApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectricityProviderController : ControllerBase
    {
        private readonly IElectricityProvider _iElectricityProvider;
        private readonly IElectricProviderPicker _iElectricProviderPicker;

        public ElectricityProviderController(
            IElectricityProvider iElectricityProvider,
            IElectricProviderPicker iElectricProviderPicker)
        {
            _iElectricityProvider = iElectricityProvider;
            _iElectricProviderPicker = iElectricProviderPicker;
        }

        // Have a controller endpoint to get the name and price of the best ElectricityProvider name and price.

        // Create power plants
        [HttpPost("powerplant")]
        public IActionResult CreatePowerProvider(PowerPlant powerPlant)
        {
            _iElectricityProvider.Subscribe(powerPlant);
            return Created("/ElectricityProviderApi/powerplant", powerPlant);
        }

        [HttpPut("/addProviderToList")]
        public IActionResult AddExistingPowerProviderToList(ElectricityProvider electricityProvider)
        {
            _iElectricProviderPicker.add(electricityProvider);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var powerPlantLists = _iElectricProviderPicker.Get();
            return Ok(powerPlantLists);
        }


    }
}
