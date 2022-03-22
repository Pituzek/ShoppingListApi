using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Db
{
    public class ElectricProvidersContext : DbContext
    {
        // tylko i wylacznie modele z folderu Db!
        public DbSet<ElectricityProvider> ElectricityProviders { get; set; }
        public DbSet<PowerPlant> PowerPlants { get; set; }
        public DbSet<ElectricProviderPicker> ElectricProviderPicker { get; set; }

        public ElectricProvidersContext(DbContextOptions<ElectricProvidersContext> options)
        {

        }

        public ElectricProvidersContext() : base(UseSqlite())
        {

        }

        private static DbContextOptions UseSqlite()
        {
            return new DbContextOptionsBuilder()
                .UseSqlite(@"DataSource=ElectricProvidersList.db;")
                .Options;
        }

    }
}
