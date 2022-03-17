using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Xunit;
using ElectricityProviderApi.Controllers;
using ElectricityProviderApi.Services;
using ElectricityProviderApi.Models;
using System.Collections.Generic;
using System.Reflection;
using ElectricProvider = ElectricityProviderApi.Services;

namespace ElectricityProvider.Tests
{
    public class ElectricityProviderTest
    {
        [Theory]
        [MemberData(nameof(CalculatePriceData))]
        public void CalculatePrice_ReturnsPriceForClosestPowerPlant(
            PowerPlant[] powerPlantList,
            Address address,
            decimal expectedPrice)
        {
            var provider = BuildProvider();
            
            foreach (var plant in powerPlantList)
            {
                provider.Subscribe(plant);
            }

            var calulatedPrice = provider.CalculatePrice(address);

            Assert.Equal(expectedPrice, calulatedPrice);
        }

        [Fact]
        public void Subscribe_AddsPowerPlantToList()
        {
            var provider = BuildProvider();
            var plant = BuildPowerPlant();

            provider.Subscribe(plant);

            Assert.Contains(plant, provider._powerPlantList);
        }

        [Fact]
        public void Unsubscribe_RemovePowerPlantFromList()
        {
            var build = BuildProviderWithPowerPlant();
            var plant = BuildPowerPlant();

            build.Unsubscribe(build._powerPlantList[0]);

            Assert.DoesNotContain(plant, build._powerPlantList);
        }

        // not recommended to test private methods, because you test them along with public ones
        // and reflection takes resources
        [Theory]
        [MemberData(nameof(LocationList))]
        public void CalculateDistance_ReturnsCorrectDistance(Location destination, Address currentPos, decimal expected)
        {
            var provider = BuildProvider();
            int precision = 2;

            var value = typeof(ElectricProvider.ElectricityProvider)
                .GetMethod("CalculateDistance", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(new ElectricProvider.ElectricityProvider(), new object[] { currentPos, destination});
            decimal distanceFromMethod = (decimal)value;
            
            Assert.Equal(expected, distanceFromMethod, 2);
        }

        public static IEnumerable<object[]> LocationList
        {
            get
            {
                yield return new object[]
                {
                    new Location() { X = 10, Y = 10, Z = 50},
                    new Address() { Location = new Location() { X = 2, Y = 5, Z = 0} },
                    50.882217m
                };
            }
        }

        private ElectricProvider.ElectricityProvider BuildProviderWithPowerPlant() => new ElectricProvider.ElectricityProvider() { _powerPlantList = { BuildPowerPlant() } };

        private PowerPlant BuildPowerPlant() => new PowerPlant() { Name = "test" };

        private ElectricProvider.ElectricityProvider BuildProvider() => new ElectricProvider.ElectricityProvider() { Name = "Enea" };

        public static IEnumerable<object[]> CalculatePriceData
        {
            get
            {
                yield return new object[] {
                    new object[] {
                        new PowerPlant() {
                        Name = "first",
                        Location = new Location() { X = 10, Y = 10, Z = 0 },
                        ElectricityPrice = 10,
                        ProducedPowerPerDay = 10
                        },
                        new PowerPlant() {
                        Name = "second",
                        Location = new Location() { X = 150, Y = 150, Z = 100 },
                        ElectricityPrice = 10,
                        ProducedPowerPerDay = 10
                        }
                    },
                    new Address()
                    {
                        Location = new Location() { X = 10, Y = 10, Z = 0 },
                        Street = "street",
                        City = "czarnobyl",
                        Country = "ukraine",
                        HouseNumber = 1
                    },
                    (decimal) 10
                };

            }
        }
    }
}
