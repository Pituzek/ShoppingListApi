using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Controllers;
using ShoppingListApi.Services;
using System;
using Xunit;

namespace ShoppingListApi.Tests
{
    public class ShoppingListControllerTests
    {
        private readonly ShoppingListController _controller;

        private readonly ITaxedShoppingListConverter _shoppingListConverter;
        private readonly IShoppingListService _shoppingListService;
        private readonly IItemsGenerator _itemsGenerator;

        // Test setup
        public ShoppingListControllerTests()
        {
            _shoppingListConverter = new TaxedShoppingListConverter(new ITaxPolicy[] { new FixedTaxPolicy(1) });
            _shoppingListService = new ShoppingListService();
            _itemsGenerator = new ItemsGenerator();

            _controller = new ShoppingListController(_shoppingListService, _itemsGenerator, _shoppingListConverter);
        }

        [Fact]
        public void Get_WhenNonExistingShoppingList_ReturnsNotFound()
        {
            const int nonExistingShoppingListId = 0;

            var response = _controller.Get(nonExistingShoppingListId);

            Assert.IsAssignableFrom<NotFoundObjectResult>(response);
        }

        [Fact]
        void Get_WhenExistingShoppingList_ReturnsOk()
        {
            const int ExistingShoppingListId = 1;
            _shoppingListService.Add(new ShoppingList() { Id = ExistingShoppingListId });

            var response = _controller.Get(ExistingShoppingListId);

            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

    }
}
