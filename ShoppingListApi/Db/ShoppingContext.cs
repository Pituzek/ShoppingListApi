using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Db
{
    public class ShoppingContext : DbContext
    {
        // data persist within DbSet (saves data state of application)
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Item> Items { get; set; }
        
        public ShoppingContext() : base(UseSqlite())
        {
        }

        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
        }

        private static DbContextOptions<ShoppingContext> UseSqlite()
        {
            return new DbContextOptionsBuilder<ShoppingContext>()
                .UseSqlite(@"DataSource=ShoppingList.db;")
                .LogTo(m => Debug.WriteLine(m))
                .Options;
        }
    }
}
