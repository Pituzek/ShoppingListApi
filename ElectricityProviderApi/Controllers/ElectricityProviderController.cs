using ElectricityProviderApi.Models;
using ElectricityProviderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityProviderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectricityProviderController : ControllerBase
    {
        private readonly IElectricityProvider _electricityProvider;
        private readonly IElectricProviderPicker _electricProviderPicker;

        public ElectricityProviderController(
            IElectricityProvider electricityProvider,
            IElectricProviderPicker electricProviderPicker)
        {
            _electricityProvider = electricityProvider;
            _electricProviderPicker = electricProviderPicker;
        }

        /// <summary>
        /// Create Electricity provider
        /// </summary>
        /// <param name="electricityProvider"></param>
        /// <returns></returns>
        [HttpPost("electricprovider")]
        public IActionResult CreateElectricityProvider(ElectricityProvider electricityProvider)
        {
            _electricProviderPicker.add(electricityProvider);
            return Created("/electricprovider", _electricProviderPicker);
        }

        /// <summary>
        /// Subscribe power plant to existing Electricity provider
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="powerPlant"></param>
        /// <returns></returns>
        [HttpPut("addpowerplant/electricprovider/{providername}")]
        public IActionResult SubscribePowerPlant(string providerName, PowerPlant powerPlant)
        {
            var selectedProvider = _electricProviderPicker.FindByName(providerName) ?? throw new System.Exception($"Didn't find existing provider with this name: {providerName}");
            selectedProvider?.Subscribe(powerPlant);
            return Ok();
        }

        /// <summary>
        /// Unsubscribe power plant from existing electricity provider
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="powerPlant"></param>
        /// <returns></returns>
        [HttpDelete("removepowerplant/electricprovider/{providername}")]
        public IActionResult RemovePowerPlant(string providerName, PowerPlant powerPlant)
        {
            var selectedProvider = _electricProviderPicker.FindByName(providerName) ?? throw new System.Exception($"Didn't find existing provider with this name: {providerName}");
            selectedProvider?.Unsubscribe(powerPlant);
            return Ok();
        }

        /// <summary>
        /// Get all electricity providers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllElectricProviders()
        {
            var electricityProviders = _electricProviderPicker.Get();
            return Ok(electricityProviders);
        }

        /// <summary>
        /// Find cheapest electricity provider, based on customer location
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet("findcheapest")]
        public IActionResult FindCheapestElectricityProvider(Address address)
        {
            var cheapest = _electricProviderPicker.FindCheapest(address);
            if (cheapest.Name == null)
            {
                return NotFound($"Cheapest electric provider for {address.Country} {address.City} {address.Street} was not found");
            }
            return Ok(cheapest);
        }
    }
}
