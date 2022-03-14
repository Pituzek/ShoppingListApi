using ElectricityProviderApi.Models;
using System.Collections.Generic;

namespace ElectricityProviderApi.Services
{
    public interface IElectricProviderPicker
    {
        void add(ElectricityProvider electricityProvider);
        ElectricityProvider FindCheapest(Address address);
        List<ElectricityProvider> Get();
        ElectricityProvider FindByName(string name);
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

        public ElectricityProvider FindByName(string name)
        {
            foreach (var provider in _electricityProvidersList)
            {
                if (provider.Name.ToLower() == name.ToLower().Trim()) return provider;
            }

            return null;
        }
    }
}
