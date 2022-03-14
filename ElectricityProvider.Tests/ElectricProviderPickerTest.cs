using System;
using Xunit;
using ElectricityProviderApi.Controllers;
using ElectricityProviderApi.Services;
using System.Collections.Generic;

namespace ElectricityProvider.Tests
{
    public class ElectricProviderPickerTest
    {
        // zrobic przez yield return
        [Fact]
        public void Get_ReturnListOfElectricityProviders()
        {
            ElectricityProviderApi.Services.ElectricityProvider firstProvider =
                 new ElectricityProviderApi.Services.ElectricityProvider() { Name = "czarnobyl" };
            ElectricityProviderApi.Services.ElectricityProvider secondProvider =
                 new ElectricityProviderApi.Services.ElectricityProvider() { Name = "enea" };
            ElectricProviderPicker providerPicker = new ElectricProviderPicker();
            providerPicker.add(firstProvider);
            providerPicker.add(secondProvider);

            List<ElectricityProviderApi.Services.ElectricityProvider> expected
                 = new List<ElectricityProviderApi.Services.ElectricityProvider>() {firstProvider, secondProvider}; 

            var collection = providerPicker.Get();

            Assert.Equal(expected, collection);
        }

        [Theory]
        [InlineData("Czarnobyl")]
        [InlineData("cZarnobyl")]
        [InlineData("cZarnobyL")]
        [InlineData("cZarnobyL ")]
        [InlineData(" cZarnobyL")]
        public void FindByName_ReturnElectricityProvider(string electricityProviderName)
        {
            ElectricityProviderApi.Services.ElectricityProvider provider =
                new ElectricityProviderApi.Services.ElectricityProvider() { Name = "czarnobyl"};
            ElectricProviderPicker providerPicker = new ElectricProviderPicker();
            providerPicker.add(provider);

            var response = providerPicker.FindByName(electricityProviderName);

            Assert.Equal(provider, response);
        }
    }
}
