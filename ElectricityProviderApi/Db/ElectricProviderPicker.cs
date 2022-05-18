using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Db
{
    public class ElectricProviderPicker
    {
        public int Id { get; set; }
        public ICollection<ElectricityProvider> ElectricityProvider { get; set; }
    }
}
