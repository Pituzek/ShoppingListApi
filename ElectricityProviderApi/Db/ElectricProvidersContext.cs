using ElectricityProviderApi.Models;
using Domain = ElectricityProviderApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityProviderApi.Db
{
    public class ElectricProvidersContext : DbContext
    {
        public DbSet<Domain.ElectricityProvider> ElectricityProviders { get; set; }
        public DbSet<PowerPlant> PowerPlants { get; set; }
        public DbSet<Domain.ElectricProviderPicker> ElectricProviderPicker { get; set; }

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
