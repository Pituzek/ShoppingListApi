﻿using System.Text;
using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Db;
using System;
using Xunit;

namespace ShoppingListApi.Tests
{
    public class DbTests : IDisposable
    {
        protected ShoppingContext Context { get; set; }

        public DbTests()
        {
            Context = new ShoppingContext(
                  new DbContextOptionsBuilder<ShoppingContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options);
        }

        [Fact]
        public void Test()
        {
            Assert.Empty(Context.Items);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
