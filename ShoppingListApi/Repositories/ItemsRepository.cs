using Db = ShoppingListApi.Db;
using Domain = ShoppingListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingListApi.Mappers;

namespace ShoppingListApi.Repositories
{
    public interface IItemsRepository
    {
        void Create(int shoppingListId, Domain.Item item);
    }

    public class ItemsRepository : IItemsRepository
    {
        private Db.ShoppingContext _context;

        public ItemsRepository(Db.ShoppingContext context)
        {
            _context = context;
        }
        public void Create(int shoppingListId, Domain.Item item)
        {
            var dbItem = item.Map();
            var shoppingList = _context.ShoppingLists.Find(shoppingListId);
            shoppingList.Items.Add(dbItem);
            _context.SaveChanges();
        }
    }
}
