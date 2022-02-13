using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db = ShoppingListApi.Db;
using Domain = ShoppingListApi.Models;

namespace ShoppingListApi.Mappers
{
    public static class ItemMappings
    {
        public static Domain.Item Map(this Db.Item Item)
        {
            return new Domain.Item
            {
                Amount = Item.Amount,
                Name = Item.Name,
                Price = Item.Price
            };
        }

        public static Db.Item Map(this Domain.Item Item)
        {
            return new Db.Item
            {
                Amount = Item.Amount,
                Name = Item.Name,
                Price = Item.Price
            };
        }
    }
}
