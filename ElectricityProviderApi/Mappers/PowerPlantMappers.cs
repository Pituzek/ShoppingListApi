using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel = ElectricityProviderApi.Models;

namespace ElectricityProviderApi.Mappers
{
    public static class PowerPlantMappers
    {
        public static DomainModel.PowerPlant Map(this Db.PowerPlant powerPlant)
        {
            return new DomainModel.PowerPlant
            {
                ElectricityPrice = powerPlant.ElectricityPrice,
                Name = powerPlant.Name,
                Location = powerPlant.Location,
                ProducedPowerPerDay = powerPlant.ProducedPowerPerDay
            };
        }

        public static Db.PowerPlant Map(this DomainModel.PowerPlant powerPlant)
        {
            return new Db.PowerPlant
            {
                ElectricityPrice = powerPlant.ElectricityPrice,
                Name = powerPlant.Name,
                Location = powerPlant.Location,
                ProducedPowerPerDay = powerPlant.ProducedPowerPerDay
            };
        }
    }
}
