using ElectricityProviderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Services
{
    public interface IElectricProviderPicker
    {
        void add(ElectricityProvider electricityProvider);
        ElectricityProvider FindCheapest(Address address);
        List<ElectricityProvider> Get();
    }

    public class ElectricProviderPicker : IElectricProviderPicker
    {
        private List<ElectricityProvider> _electricityProvidersList = new List<ElectricityProvider>();

        public void add(ElectricityProvider electricityProvider)
        {
            _electricityProvidersList.Add(electricityProvider);
        }

        public ElectricityProvider FindCheapest(Address address)
        {
            decimal max = decimal.MaxValue;
            ElectricityProvider cheapestProvider = new ElectricityProvider();

            foreach (var provider in _electricityProvidersList)
            {
                decimal tmp = provider.CalculatePrice(address);
                if (tmp < max)
                {
                    max = tmp;
                    cheapestProvider = provider;
                }
            }

            return cheapestProvider;
        }

        public List<ElectricityProvider> Get()
        {
            return _electricityProvidersList;
        }
    }
}
