using System;
using Xunit;
using ElectricityProviderApi.Controllers;
using ElectricityProviderApi.Services;
using ElectricityProviderApi.Models;
using System.Collections.Generic;
using ElectricProvider = ElectricityProviderApi.Services;

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
            ElectricProvider.ElectricityProvider firstProvider,
            ElectricProvider.ElectricityProvider secondProvider,
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
            ElectricProvider.ElectricityProvider firstProvider,
            ElectricProvider.ElectricityProvider secondProvider)
        {
            // Arrange
            ElectricProviderPicker providerPicker = new ElectricProviderPicker();
            providerPicker.add(firstProvider);
            providerPicker.add(secondProvider);

            List<ElectricProvider.ElectricityProvider> expected
                = new List<ElectricProvider.ElectricityProvider>() { firstProvider, secondProvider };

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
            ElectricProvider.ElectricityProvider provider =
                new ElectricProvider.ElectricityProvider() { Name = "czarnobyl" };
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
                        new ElectricProvider.ElectricityProvider() { Name = "czarnobyl"},
                        new ElectricProvider.ElectricityProvider() { Name = "enea" }
                };

            }
        }

        public static IEnumerable<object[]> ExpectedCheapestProvider
        {
            get
            {
                yield return new object[] {
                        new ElectricProvider.ElectricityProvider() {
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
                        new ElectricProvider.ElectricityProvider() { 
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
