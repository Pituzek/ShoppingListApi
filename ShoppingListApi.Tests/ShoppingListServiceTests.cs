using ShoppingListApi.Services;
using System;
using Xunit;

namespace ShoppingListApi.Tests
{
    public class ShoppingListServiceTests
    {
        [Fact]
        public void RemoveShoppinhList_WhenNonExistingShoppingListId_ThrowsArgumentException()
        {
            const int nonExistingShoppingListId = 0;
            var service = new ShoppingListService();

            Action removeNonExistingShoppingList = () => service.RemoveShoppingList(nonExistingShoppingListId);

            // Verify exception is thrown
            Assert.Throws<ArgumentException>(removeNonExistingShoppingList);
        }

        [Fact]
        public void RemoveShoppinhList_WhenShoppingListExists_RemovesIt()
        {
            const int existingShoppingListId = 1;
            var service = new ShoppingListService();
            var removedShoppingList = new ShoppingList() { Id = existingShoppingListId };
            service.Add(removedShoppingList);

            service.RemoveShoppingList(existingShoppingListId);

            // Verify exception is thrown
            var shoppingLists = service.Get();
            Assert.DoesNotContain(removedShoppingList, shoppingLists);
        }
    }
}
