using ElectricityProviderApi.Controllers;
using ElectricityProviderApi.Models;
using ElectricityProviderApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ElectricProvider = ElectricityProviderApi.Services;

namespace ElectricityProvider.Tests
{
    public class ElectricityProviderControllerTest
    {
        private readonly ElectricityProviderController _controller;

        private readonly IElectricityProvider _electricityProvider;
        private readonly IElectricProviderPicker _electricProviderPicker;

        public ElectricityProviderControllerTest()
        {
            _electricityProvider = new ElectricProvider.ElectricityProvider() { Name = "ProviderName" };
            _electricProviderPicker = new ElectricProviderPicker();
            _electricProviderPicker.add((ElectricProvider.ElectricityProvider)_electricityProvider);

            _controller = new ElectricityProviderController(_electricityProvider, _electricProviderPicker);
        }

        [Fact]
        public void CreateElectricityProvider_WhenCreatingElectricityProvider_ReturnsCreated()
        {
            var response = _controller.CreateElectricityProvider(new ElectricProvider.ElectricityProvider());

            Assert.IsAssignableFrom<CreatedResult>(response);
        }

        [Fact]
        public void SubscribePowerPlant_AddsPowerPlantToProvider_ReturnsOk()
        {
            PowerPlant powerPlant = BuildPowerPlant();

            var response = _controller.SubscribePowerPlant(_electricityProvider.GetProviderName(), powerPlant);

            Assert.IsAssignableFrom<OkResult>(response);
        }

        [Fact]
        public void RemovePowerPlant_RemovesPowerPlantFromProvider_ReturnOk()
        {
            PowerPlant powerPlant = BuildPowerPlant();

            var response = _controller.RemovePowerPlant(_electricityProvider.GetProviderName(), powerPlant);

            Assert.IsAssignableFrom<OkResult>(response);
        }

        [Fact]
        public void GetAllElectricProviders_GetListOfAllProviders_ReturnOk()
        {
            var result = _controller.GetAllElectricProviders();

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public void FindCheapest_WhenNonExistingProviderList_ReturnsNotFound()
        {
            Address address = BuildAddress();

            var response = _controller.FindCheapestElectricityProvider(address);

            Assert.IsAssignableFrom<NotFoundObjectResult>(response);
        }

        [Fact]
        public void FindCheapest_WhenExistingProviderList_ReturnsOk()
        {
            Address address = BuildAddress();
            PowerPlant powerPlant = BuildPowerPlant();
            _controller.SubscribePowerPlant(_electricityProvider.GetProviderName(), powerPlant);
            
            var response = _controller.FindCheapestElectricityProvider(address);

            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

        private Address BuildAddress() => new Address() { Street = "sezamkowa", Location = new Location() { X = 10, Y = 10, Z = 0 }, City = "Poznan", Country = "Poland", HouseNumber = 1 };
        private PowerPlant BuildPowerPlant() => new PowerPlant() { Name = "test", Location = new Location() { X = 30, Y = 10, Z = 0 } };
    }
}
