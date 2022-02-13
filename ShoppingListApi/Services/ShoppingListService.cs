using ShoppingListApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Services
{
    public interface IShoppingListService
    {
         decimal CalculateTotalCost();
         void Add(ShoppingList shoppingList);
         IEnumerable<ShoppingList> Get();
         ShoppingList FindShoppingList(int id);
         void RemoveShoppingList(int id);
         void UpdateShoppingListName(int id, string name);
         void UpdateShoppingList(int id, ShoppingList shoppingList);
         void AddItem(int shoppingListId, Item item);
    }

    public class ShoppingListService : IShoppingListService
    {
        private Dictionary<int, ShoppingList> _shoppingLists = new Dictionary<int, ShoppingList>();
        private readonly ShoppingListRepository _repository;

        public ShoppingListService(ShoppingListRepository repository)
        {
            _repository = repository;
        }

        public decimal CalculateTotalCost()
        {
            return _shoppingLists.Values
                .Select(sl => sl.CalculateTotalCost())
                .Sum();
        }

        public void Add(ShoppingList shoppingList)
        {
             _shoppingLists.Add(shoppingList.Id, shoppingList);
        }

        public IEnumerable<ShoppingList> Get()
        {
            return _shoppingLists.Values;
        }

        public ShoppingList FindShoppingList(int id)
        {
            if (_shoppingLists.ContainsKey(id))
            {
                return _shoppingLists[id];
            }

            return null;
        }

        public IEnumerable<ShoppingList> GetByName(string name)
        {
            //return _shoppingLists.Values.Where(list =>
            //    list.ShopName.Equals(name, StringComparison.OrdinalIgnoreCase));
            return _repository.GetByName(name);
        }

        public void RemoveShoppingList(int id)
        {
            var shoppingList = FindShoppingList(id);
            if (shoppingList == null) throw new ArgumentException($"Shopping list by id {id} was not found.");

            _shoppingLists.Remove(shoppingList.Id);
        }

        public void UpdateShoppingListName(int id, string name)
        {
            var oldShoppingList = FindShoppingList(id);
            if (oldShoppingList == null)
            {
                throw new ArgumentException($"Shopping list by id {id} was not found");
            }

            oldShoppingList.ShopName = name;
        }

        public void UpdateShoppingList(int id, ShoppingList shoppingList)
        {
            var oldShoppingList = FindShoppingList(id);

            if (shoppingList == null)
            {
                throw new ArgumentException($"Shopping list by id {id} was not found");
            }

            oldShoppingList.Update(shoppingList);
        }

        public void AddItem(int shoppingListId, Item item)
        {
            var shoppingList = FindShoppingList(shoppingListId);
            if (shoppingList == null)
            {
                throw new ArgumentException($"Shopping list by id {shoppingListId} was not found");
            }

            shoppingList.Add(item);
        }
    }
}
