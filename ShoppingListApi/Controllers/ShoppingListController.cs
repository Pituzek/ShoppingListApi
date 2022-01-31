using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private static List<ShoppingList> _shoppingLists = new List<ShoppingList>();
        private static Random _random = new Random();

        [HttpGet("total")]
        public IActionResult GetTotalPrice()
        {
            decimal totalCost = 0;

            foreach (var shoppingList in _shoppingLists)
            {
                totalCost += shoppingList.CalculateTotalCost();
            }

            return Ok(totalCost);
        }

        // Http post allows using a POST http verb
        // IActionResult allows different types of result to be returned.
        // Specifically, it allows different status codes to be returned.
        // IF we dont use IActionResult, we will be left with just 2 options:
        // - either 200 or 500
        // With IActionResult we can return any status code possible.
        // Usually is used to create new resources or running comlex queries.
        [HttpPost]
        public IActionResult Create(ShoppingList shoppingList)
        {
            _shoppingLists.Add(shoppingList);
            return Created("/shoppinglist", shoppingList);
        }

        [HttpGet("item/random")]
        public IActionResult GetRandomItem()
        {
            var bytes = new byte[100];
            _random.NextBytes(bytes);
            var randomName = Encoding.Unicode.GetString(bytes);

            var item = new Item
            {
                Amount = _random.NextDouble() * 100,
                Price = (decimal)(_random.NextDouble() * 100),
                Name = randomName
            };

            return Ok(item);
        }

        // Allows using a Get http verb.
        [HttpGet]
        public IActionResult Get()
        {
            // Never return resource directly, always return it through Ok() or similar.
            return Ok(_shoppingLists);
        }

        // Allows using a Get http verb.
        // {id} - is a custom route -  a placeholder for the value that comes as an argument.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var shoppingList = FindShoppingList(id);
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
            var shoppingList = FindShoppingList(shoppingListId);
            if (shoppingList == null)
            {
                return NotFound($"Shopping list by id {shoppingListId} was not found");
            }

            shoppingList.Items.Add(item);

            return Ok();
        }

        // Allows using PUT http verb.
        // Ovverides resource with a new values.
        [HttpPut("{id}")]
        public IActionResult UpdateShoppingList(int id, ShoppingList shoppingList)
        {
            var oldShoppingList = FindShoppingList(id);
            if (shoppingList == null)
            {
                return NotFound($"Shopping list by id {id} was not found");
            }

            oldShoppingList.Items = shoppingList.Items;
            oldShoppingList.Address = shoppingList.Address;
            oldShoppingList.ShopName = shoppingList.ShopName;
            oldShoppingList.TotalPrice = shoppingList.TotalPrice;

            return Ok(oldShoppingList);
        }

        [HttpPatch("{id}/updateName")]
        public IActionResult UpdateShoppingListName(int id, ShoppingList shoppingList)
        {
            var oldShoppingList = FindShoppingList(id);
            if (shoppingList == null)
            {
                return NotFound($"Shopping list by id {id} was not found");
            }

            oldShoppingList.ShopName = shoppingList.ShopName;

            return Ok(oldShoppingList);
        }

        // Allows using a DELETE http verb.
        // Deletes resource.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var shoppingList = FindShoppingList(id);
            if (shoppingList == null)
            {
                return NotFound($"Shopping list by id {id} was not found");
            }

            _shoppingLists.Remove(shoppingList);

            return Ok();
        }

        private ShoppingList FindShoppingList(int id)
        {
            foreach (var list in _shoppingLists)
            {
                if (list.Id == id)
                {
                    return list;
                }
            }

            return null;
        }
    }
}
