using DomainModels = ElectricityProviderApi.Models;
using DomainServices = ElectricityProviderApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricityProviderApi.Mappers;

namespace ElectricityProviderApi.Repositories
{
    public class PowerPlantRepository
    {
        private Db.ElectricProvidersContext _context;

        public PowerPlantRepository(Db.ElectricProvidersContext context)
        {
            _context = context;
        }

        public void SubscribePowerPlant(string providerName, DomainModels.PowerPlant powerPlant)
        {
            //var dbProvider = _context
            //    .ElectricityProviders
            //    .Where(p => p.Name == providerName)
            //    .Select(p => p).ToList();
            //var t = dbProvider[0].Map();

            //_context.ElectricityProviders.ToList().Find(p => p.Name == providerName)._powerPlantList.Add(powerPlant.Map());

            var dbProvider = _context.ElectricityProviders.ToList().Find(p => p.Name == providerName);
            dbProvider._powerPlantList.Add(powerPlant.Map());
            
            _context.Add(dbProvider);
            _context.SaveChanges();
        }

        public void RemovePowerPlant(string providerName, DomainModels.PowerPlant powerPlant)
        {
            var dbProvider = _context.ElectricityProviders.ToList().Find(p => p.Name == providerName);
            dbProvider._powerPlantList.Remove(powerPlant.Map());

            _context.SaveChanges();
        }
    }
}
