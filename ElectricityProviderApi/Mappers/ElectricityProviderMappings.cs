using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainService = ElectricityProviderApi.Services;
using Db = ElectricityProviderApi.Db;

namespace ElectricityProviderApi.Mappers
{
    public static class ElectricityProviderMappings
    {
        public static DomainService.ElectricityProvider Map(this Db.ElectricityProvider electricityProvider)
        {
            DomainService.ElectricityProvider dbElectricityProvider = new DomainService.ElectricityProvider(null);
            dbElectricityProvider.Name = electricityProvider.Name;
            dbElectricityProvider._powerPlantList = electricityProvider._powerPlantList.Select(p => p.Map()).ToList();

            return dbElectricityProvider;
        }

        public static Db.ElectricityProvider Map(this DomainService.ElectricityProvider electricityProvider)
        {
            Db.ElectricityProvider dbElectricityProvider = new Db.ElectricityProvider();
            dbElectricityProvider.Name = electricityProvider.Name;
            dbElectricityProvider._powerPlantList = electricityProvider._powerPlantList.Select(p => p.Map()).ToList();

            return dbElectricityProvider;
        }
    }
}
