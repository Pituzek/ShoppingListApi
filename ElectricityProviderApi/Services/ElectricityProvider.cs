using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricityProviderApi.Models;

namespace ElectricityProviderApi.Services
{
    public interface IProvider
    {
        void Subscribe(PowerPlant plant); //- subscribes to a single power plant.If powerplant is already subscribed, throws an exception.
        void Unsubscribe(PowerPlant plant); //- unsubscribes from a subscribed power plant. If no powerplant is subscribed- does nothing.
        decimal CalculatePrice(Address address); //- use some arbitrary formula to calculate the price the provider will charge for elecrticity.Location should be a factor.
    }

    public class ElectricityProvider : IProvider
    {
        private PowerPlant _powerProvider { get; set; }
        public string? Name { get; set; }

        // im dalej od lokalizacji danej elektrowni, tym wiekszy koszt energii
        // dodac metode obliczajaca odleglosc danego dostawcy, od danego adresu klienta, i na tej podstawie obliczac cene energii
        public decimal CalculatePrice(Address address)
        {
            decimal distProviderToReceiver = CalculateDistance(address.Location.X, address.Location.Y, address.Location.Z);

            if (distProviderToReceiver > 50 && distProviderToReceiver < 100) return _powerProvider.ElectricityPrice * 1.1m;
            if (distProviderToReceiver >= 100) return _powerProvider.ElectricityPrice * 1.5m;

            return _powerProvider.ElectricityPrice;
        }

        public void Subscribe(PowerPlant plant)
        {
            if (this.Name != null)
            {
                throw new ArgumentException("Another plant is already subscribed to this provider.");
            }

            this.Name = plant.Name;
            this._powerProvider = plant;
        }

        public void Unsubscribe(PowerPlant plant)
        {
            if (this.Name == plant.Name)
            {
                this.Name = null;
                this._powerProvider = null;
            }
        }

        private decimal CalculateDistance(int addressX, int addressY, int addressZ)
        {
            double totalDistance = -1;
            totalDistance = Math.Sqrt( 
                Math.Pow(addressX - _powerProvider.Location.X, 2) +
                Math.Pow(addressY - _powerProvider.Location.Y, 2) +
                Math.Pow(addressZ - _powerProvider.Location.Z, 2)
                );

            return (decimal)totalDistance;
        }
    }
}
