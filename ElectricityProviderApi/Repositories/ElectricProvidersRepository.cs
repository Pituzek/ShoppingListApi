
using DomainModel = ElectricityProviderApi.Models;
using DomainService = ElectricityProviderApi.Services;
using Db = ElectricityProviderApi.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        }

        public IEnumerable<DomainService.ElectricityProvider> GetAllElectricProviders()
        {
            return _context.ElectricProviderPicker.ToList().Select(p => p.Map());
        }

        public DomainService.ElectricityProvider FindCheapestElectricityProvider(DomainModel.Address address)
        {
            return null;
        }
    }
}
