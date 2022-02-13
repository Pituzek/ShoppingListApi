using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingListApi.Models;
using ShoppingListApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingListApi.Controllers
{
    // This is an attribute.
    // It adds metadata to a class.
    // Something, somewhere will read this attribute
    // and apply extra logic to the class.
    [ApiController]
    // Discoverabke endpoint: api/ShoppingList
    [Route("api/[controller]")]
    // : ControllerBase is called inheritance.
    // we take methods and state from ControllerBase
    // and now we are able to use them.
    // Jesli zmienie nazwe klasy ShoppingListController, na np ShoppingListMISCController
    // to wtedy bede musial wywolac adres:
    // https://localhost:44338/api/ShoppingListMISC zamiast https://localhost:44338/api/ShoppingList
    // musi byc atrybut : [Route("api/[controller]")], jesli uzyje [Route("api/nazwa_na_sztywno_nie_z_klasy")]
    public class ShoppingListController : ControllerBase
    {
        private readonly ITaxedShoppingListConverter _shoppingListConverter;
        private readonly IShoppingListService _shoppingListService;
        private readonly IItemsGenerator _itemsGenerator;

        public ShoppingListController(
            IShoppingListService shoppingListService,
            IItemsGenerator itemsGenerator,
            ITaxedShoppingListConverter shoppingListConverter)
        {
            _shoppingListService = shoppingListService;
            _itemsGenerator = itemsGenerator;
            _shoppingListConverter = shoppingListConverter;
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var shoppingLists = _shoppingListService.Get();
            return Ok(shoppingLists);
        }

        [HttpGet("total")]
        public IActionResult GetTotalPrice()
        {
            var totalCost = _shoppingListService.CalculateTotalCost();
            return Ok(totalCost);
        }

        // Http post allows using a POST http verb
        // IActionResult allows different types of result to be returned.
        // Specifically, it allows different status codes to be returned.
        // IF we dont use IActionResult, we will be left with just 2 options:
        // - either 200 or 500
        // With IActionResult we can return any status code possible.
        // Usually is used to create new resources or running comlex queries.
        [HttpPost("basic")]
        public IActionResult CreateTaxed(ShoppingList shoppingList)
        {
            _shoppingListService.Add(shoppingList);
            return Created("/shoppinglist/basic", shoppingList);
        }

        [HttpPost("taxed")]
        public IActionResult Create(ShoppingList shoppingList)
        {
            var taxedShoppingList = _shoppingListConverter.Converts(shoppingList);
            _shoppingListService.Add(taxedShoppingList);
            return Created("/shoppinglist/taxed", taxedShoppingList);
        }

        [HttpGet("item/random")]
        public IActionResult GetRandomItem()
        {
            var item = _itemsGenerator.Generate();
            return Ok(item);
        }

        // Allows using a Get http verb.
        [HttpGet]
        public IActionResult Get()
        {
            // Never return resource directly, always return it through Ok() or similar.
            var shoppingLists =_shoppingListService.Get();
            return Ok(shoppingLists);
        }

        // Allows using a Get http verb.
        // {id} - is a custom route -  a placeholder for the value that comes as an argument.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var shoppingList = _shoppingListService.FindShoppingList(id);
            if (shoppingList == null)
            {
                return NotFound($"Shopping list by id {id} was not found");
            }

            return Ok(shoppingList);
        }

        // Allow using a PATCH http verb
        // Patch updates a part of a resource.
        [HttpPatch("{shoppingListId}")]
        public IActionResult AddItem(int shoppingListId, Item item)
        {
            try
            {
                _shoppingListService.AddItem(shoppingListId, item);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

        // Allows using PUT http verb.
        // Ovverides resource with a new values.
        [HttpPut("{id}")]
        public IActionResult UpdateShoppingList(int id, ShoppingList shoppingList)
        {
            try
            {
                _shoppingListService.UpdateShoppingList(id, shoppingList);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

        [HttpPatch("{id}/updateName")]
        public IActionResult UpdateShoppingListName(int id, ShoppingList shoppingList)
        {
            try
            {
                _shoppingListService.UpdateShoppingListName(id, shoppingList.ShopName);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

        // Allows using a DELETE http verb.
        // Deletes resource.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _shoppingListService.RemoveShoppingList(id);
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

    }
}
