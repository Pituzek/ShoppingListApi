using System;

namespace ElectricityProviderApi.Models
{
    public class PowerPlant
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public decimal ElectricityPrice { get; set; }
        public decimal ProducedPowerPerDay { get; set; }

        public override string ToString()
        {
            return String.Format(
                $"\nName: {Name} " +
                $"\nLocation:" +
                $"\n  X:{Location.X} " +
                $"\n  Y:{Location.Y} " +
                $"\n  Z:{Location.Z} " +
                $"\nElectricityPrice: {ElectricityPrice} " +
                $"\nProducedPowerPerDay: {ProducedPowerPerDay}"
                );
        }
    }
}
