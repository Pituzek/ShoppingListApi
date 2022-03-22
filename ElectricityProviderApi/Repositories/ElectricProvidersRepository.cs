using DomainModel = ElectricityProviderApi.Models;
using DomainService = ElectricityProviderApi.Services;
using Db = ElectricityProviderApi.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricityProviderApi.Mappers;

namespace ElectricityProviderApi.Repositories
{
    public class ElectricProvidersRepository
    {
        private Db.ElectricProvidersContext _context;

        public ElectricProvidersRepository(Db.ElectricProvidersContext context)
        {
            _context = context;
        }

        public void CreateElectricityProvider(DomainService.ElectricityProvider electricityProvider)
        {
            // dodaj providera do bazy
            var dbElectricityProviderList = electricityProvider.Map();
            _context.Add(dbElectricityProviderList);
            _context.SaveChanges();
        }

        public IEnumerable<DomainService.ElectricityProvider> GetAllElectricProviders()
        {
            // zwroc dane providera z bazy
            return _context.ElectricityProviders.Select(p => p.Map()).ToList();
            //return (IEnumerable<DomainService.ElectricityProvider>)_context.ElectricityProviders;
        }

        public DomainService.ElectricityProvider FindCheapestElectricityProvider(DomainModel.Address address)
        {
            // to powinno isc z ProviderPicker
            return null;
        }
    }
}
