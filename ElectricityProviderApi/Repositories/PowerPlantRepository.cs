using ElectricityProviderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Repositories
{
    public class PowerPlantRepository
    {
        private Db.ElectricProvidersContext _context;

        public PowerPlantRepository(Db.ElectricProvidersContext context)
        {
            _context = context;
        }

        public void SubscribePowerPlant(string providerName, PowerPlant powerPlant)
        {
           // var test = _context.ElectricityProviders;
                _context.Add(powerPlant);
        }

        public void RemovePowerPlant(string providerName, PowerPlant powerPlant)
        {
            _context.RemovePowerPlant(providerName, powerPlant);
        }
    }
}
