using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Models
{
    public class PowerPlant
    {
        public Location Location { get; set; }
        public decimal ElectricityPrice { get; set; }
        public decimal ProducedPowerPerDay { get; set; }
        public string Name { get; set; }
    }
}
