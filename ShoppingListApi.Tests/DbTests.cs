using System.Text;
using Microsoft.EntityFrameworkCore.InMemory;
using ShoppingListApi.Db;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListApi.Tests
{
    public abstract class DbTests : IDisposable
    {
        protected ShoppingContext Context { get; set; }

        public DbTests()
        {
            Context = new ShoppingContext(
                  new DbContextOptionsBuilder<ShoppingContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
