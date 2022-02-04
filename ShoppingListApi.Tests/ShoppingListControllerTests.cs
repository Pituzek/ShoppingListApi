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
            const int existingShoppingListId = 1;
            _shoppingListService.Add(new ShoppingList() { Id = existingShoppingListId });

            var response = _controller.Get(existingShoppingListId);

            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

        [Fact]
        public void GetByName_WhenNonExistingName_ReturnsOk()
        {
            const string nonExistingShoppingListName = "This does not exist";

            var response = _controller.GetByName(nonExistingShoppingListName);

            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

        [Theory]
        [InlineData("Existing")]
        [InlineData("existing")]
        [InlineData("existIng")]
        public void GetByName_WhenExistingName_ReturnsOk(string shopName)
        {
            var response = _controller.GetByName(shopName);

            Assert.IsAssignableFrom<OkObjectResult>(response);
        }
    }
}
