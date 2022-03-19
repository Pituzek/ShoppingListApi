using ElectricityProviderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Db
{
    public class PowerPlant
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public decimal ElectricityPrice { get; set; }
        public decimal ProducedPowerPerDay { get; set; }
        public ElectricityProvider ElectricityProvider { get; set; }
    }
}
