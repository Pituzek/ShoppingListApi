﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Services
{
    public interface IShoppingListService
    {
         decimal CalculateTotalCost();
         void Add(ShoppingList shoppingList);
         List<ShoppingList> Get();
         ShoppingList FindShoppingList(int id);
         void RemoveShoppingList(int id);
         void UpdateShoppingListName(int id, string name);
         void UpdateShoppingList(int id, ShoppingList shoppingList);
         void AddItem(int shoppingListId, Item item);
    }

    public class ShoppingListService : IShoppingListService
    {
        private List<ShoppingList> _shoppingLists = new List<ShoppingList>();

        public decimal CalculateTotalCost()
        {
            return _shoppingLists
                .Select(sl => sl.CalculateTotalCost())
                .Sum();
        }

        public void Add(ShoppingList shoppingList)
        {
             _shoppingLists.Add(shoppingList);
        }

        public List<ShoppingList> Get()
        {
            return _shoppingLists;
        }

        public ShoppingList FindShoppingList(int id)
        {
            return _shoppingLists.FirstOrDefault(list => list.Id == id);
        }

        public IEnumerable<ShoppingList> GetByName(string name)
        {
            return _shoppingLists.Where(list =>
                list.ShopName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void RemoveShoppingList(int id)
        {
            var shoppingList = FindShoppingList(id);
            if (shoppingList == null) throw new ArgumentException($"Shopping list by id {id} was not found.");

            _shoppingLists.Remove(shoppingList);
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
