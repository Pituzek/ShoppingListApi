using System;
using Xunit;
using ElectricityProviderApi.Controllers;
using ElectricityProviderApi.Services;
using ElectricityProviderApi.Models;
using System.Collections.Generic;

namespace ElectricityProvider.Tests
{
    public class ElectricProviderPickerTest
    {
        /// <summary>
        /// Testing FindCheapest function
        /// </summary>
        /// <param name="firstProvider"></param>
        /// <param name="secondProvider"></param>
        /// <param name="address"></param>
        [Theory]
        [MemberData(nameof(ExpectedCheapestProvider))]
        public void FindCheapest_ReturnProviderWithLowestPrice(
            ElectricityProviderApi.Services.ElectricityProvider firstProvider,
            ElectricityProviderApi.Services.ElectricityProvider secondProvider,
            Address address)
        {
            ElectricProviderPicker providerPicker = new ElectricProviderPicker();
            providerPicker.add(firstProvider);
            providerPicker.add(secondProvider);

            var foundCheapest = providerPicker.FindCheapest(address);

            Assert.Equal(secondProvider, foundCheapest);
        }

        [Theory]
        [MemberData(nameof(ExpectedElectricProviders))]
        public void Get_ReturnListOfElectricityProviders(
            ElectricityProviderApi.Services.ElectricityProvider firstProvider,
            ElectricityProviderApi.Services.ElectricityProvider secondProvider)
        {
            // Arrange
            ElectricProviderPicker providerPicker = new ElectricProviderPicker();
            providerPicker.add(firstProvider);
            providerPicker.add(secondProvider);

            List<ElectricityProviderApi.Services.ElectricityProvider> expected
                = new List<ElectricityProviderApi.Services.ElectricityProvider>() { firstProvider, secondProvider };

            // Act
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
                new ElectricityProviderApi.Services.ElectricityProvider() { Name = "czarnobyl" };
            ElectricProviderPicker providerPicker = new ElectricProviderPicker();
            providerPicker.add(provider);

            var response = providerPicker.FindByName(electricityProviderName);

            Assert.Equal(provider, response);
        }

        public static IEnumerable<object[]> ExpectedElectricProviders
        {
            get
            {
                yield return new object[] {
                        new ElectricityProviderApi.Services.ElectricityProvider() { Name = "czarnobyl"},
                        new ElectricityProviderApi.Services.ElectricityProvider() { Name = "enea" }
                };

            }
        }

        public static IEnumerable<object[]> ExpectedCheapestProvider
        {
            get
            {
                yield return new object[] {
                        new ElectricityProviderApi.Services.ElectricityProvider() {
                            Name = "czarnobyl",
                            _powerPlantList = {
                                                new PowerPlant() {
                                                    Name = "First",
                                                    Location = new Location() { X = 50, Y = 50, Z = 0 },
                                                    ElectricityPrice = 30
                                                },
                                                  new PowerPlant() {
                                                    Name = "Second",
                                                    Location = new Location() { X = 5, Y = 5, Z = 0 },
                                                    ElectricityPrice = 15
                                                }
                            }

                        },
                        new ElectricityProviderApi.Services.ElectricityProvider() { 
                            Name = "enea",
                            _powerPlantList = {
                                                new PowerPlant() {
                                                    Name = "First",
                                                    Location = new Location() { X = 10, Y = 10, Z = 0 },
                                                    ElectricityPrice = 10
                                                },
                                                  new PowerPlant() {
                                                    Name = "Second",
                                                    Location = new Location() { X = 20, Y = 20, Z = 0 },
                                                    ElectricityPrice = 5
                                                }
                            }
                        },
                        new Address()
                        {
                            Country = "Ukraine",
                            City = "Czarnobyl",
                            Street = "Sezamkowa",
                            HouseNumber = 1,
                            Location = new Location() { X = 20, Y = 20, Z = 0 }
                        }
                };
            }
        }
    }
}
