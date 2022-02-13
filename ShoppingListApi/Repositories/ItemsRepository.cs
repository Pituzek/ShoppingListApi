using ShoppingListApi.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Repositories
{
    public class ItemsRepository
    {
        private ShoppingContext _context;

        public ItemsRepository(ShoppingContext context)
        {
            _context = context;
        }
        public void AddItem(int shoppingListId, Item item)
        {

        }
    }
}
