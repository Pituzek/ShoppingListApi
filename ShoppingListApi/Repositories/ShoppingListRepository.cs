using System.Collections.Generic;
using Db = ShoppingListApi.Db;
using System.Linq;
using Domain = ShoppingListApi.Models;

namespace ShoppingListApi.Repositories
{
    public class ShoppingListRepository
    {
        private Db.ShoppingContext _context;

        public ShoppingListRepository(Db.ShoppingContext context)
        {
            _context = context;
        }

        public Domain.ShoppingList GetByName(string name)
        {
            return _context
                .ShoppingLists
                .FirstOrDefault(list => list.ShopName == name);


        }

        public void CreateTaxed(Domain.ShoppingList shoppingList)
        {

        }

        public void Create(Domain.ShoppingList shoppingList)
        {

        }

        public IEnumerable<Domain.ShoppingList> Get()
        {
            return null;
        }

        public Domain.ShoppingList Get(int id)
        {
            return null;
        }

        public void UpdateShoppingList(int id, Domain.ShoppingList shoppingList)
        {

        }

        public void UpdateShoppingListName(int id, Domain.ShoppingList shoppingList)
        {

        }

        public void Delete(int id)
        {

        }
    }
}
