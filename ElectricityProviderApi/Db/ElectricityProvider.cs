using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Db
{
    public class ElectricityProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PowerPlant> _powerPlantList { get; set; } = new List<PowerPlant>();
    }
}
