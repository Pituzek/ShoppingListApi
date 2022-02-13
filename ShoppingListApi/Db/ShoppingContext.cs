using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Db
{
    public class ShoppingContext : DbContext
    {
        // data persist within DbSet (saves data state of application)
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Item> Items { get; set; }

        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
        }

        public ShoppingContext() : base(UseSqlite())
        {
        }

        private static DbContextOptions UseSqlite()
        {
            return new DbContextOptionsBuilder()
                .UseSqlite(@"DataSource=ShoppingList.db;")
                .Options;
        }
    }
}
