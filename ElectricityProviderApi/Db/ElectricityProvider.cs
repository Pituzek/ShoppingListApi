using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Db
{
    public class ElectricityProvider
    {
        public string Name { get; set; }
        public List<PowerPlant> _powerPlantList { get; set; }
    }
}
