using System;
using System.Collections.Generic;
using System.Linq;
using ElectricityProviderApi.Models;
using ElectricityProviderApi.Repositories;

namespace ElectricityProviderApi.Services
{
    public interface IElectricityProvider
    {
        void Subscribe(PowerPlant plant);
        void Unsubscribe(PowerPlant plant);
        decimal CalculatePrice(Address address);
        string GetProviderName();
    }

    public class ElectricityProvider : IElectricityProvider
    {
        public string Name { get; set; }
        public List<PowerPlant> _powerPlantList { get; set; } = new List<PowerPlant>();

        private readonly PowerPlantRepository _repository;

        public ElectricityProvider(PowerPlantRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Calculate cost based on distance between provider location, and customer address
        /// Closest power plant, can provide best prices for electricity
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public decimal CalculatePrice(Address address)
        {
            PowerPlant closestPowerPlant = default;
            decimal closestDistance = decimal.MaxValue;
            decimal distProviderToReceiver = -1;

            foreach (var powerPlant in _powerPlantList)
            {
                var tmpDistance = CalculateDistance(address, powerPlant.Location);
                if (tmpDistance < closestDistance)
                {
                    closestDistance = tmpDistance;
                    closestPowerPlant = powerPlant;
                    distProviderToReceiver = closestDistance;
                }
            }

            if (distProviderToReceiver > 50 && distProviderToReceiver < 100) return closestPowerPlant.ElectricityPrice * 1.1m;
            if (distProviderToReceiver >= 100) return closestPowerPlant.ElectricityPrice * 1.5m;

            return closestPowerPlant.ElectricityPrice;
        }

        /// <summary>
        /// To get provider name from controller tests
        /// </summary>
        /// <returns></returns>
        public string GetProviderName()
        {
            return this.Name;
        }

        /// <summary>
        /// Add new power plant to this provider (single power plant, can be added only once).
        /// </summary>
        /// <param name="plant"></param>
        public void Subscribe(PowerPlant plant)
        {
            var ok = _powerPlantList.Any(p => p.Name == plant.Name);
            if (ok)
            {
                throw new Exception($"This power plant is already subscribed: {plant.ToString()}");
            }
            else
            {
                this._powerPlantList.Add(plant);
            }
        }

        /// <summary>
        /// Remove power plant from this provider
        /// </summary>
        /// <param name="plant"></param>
        public void Unsubscribe(PowerPlant plant)
        {
            foreach (var subscribedPlant in _powerPlantList)
            {
                if (subscribedPlant.Name == plant.Name)
                {
                    this._powerPlantList?.Remove(subscribedPlant);
                    break;
                }
            }
        }

        /// <summary>
        /// Calculate distance between two points
        /// </summary>
        /// <param name="address"></param>
        /// <param name="powerPlantLocation"></param>
        /// <returns></returns>
        private decimal CalculateDistance(Address address, Location powerPlantLocation)
        {
            double totalDistance = -1;
            totalDistance = Math.Sqrt(
                Math.Pow(address.Location.X - powerPlantLocation.X, 2) +
                Math.Pow(address.Location.Y - powerPlantLocation.Y, 2) +
                Math.Pow(address.Location.Z - powerPlantLocation.Z, 2)
                );

            return (decimal)totalDistance;
        }
    }
}
