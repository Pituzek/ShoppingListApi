using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShoppingListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonListController : ControllerBase
    {
        private static List<PersonList> _personList = new List<PersonList>();

        [HttpPost]
        public IActionResult Create(PersonList personList)
        {
            _personList.Add(personList);
            return Created("/personlist", personList);
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Never return resource directly, always return it through Ok() or similar.
            return Ok(_personList);
        }

        // parametr int personListId musi byc wpisany do HttpPatch ! [HttpPatch("{personListId}")]
        [HttpPatch("{personListId}")]
        public IActionResult AddPerson(int personListId, Person person)
        {
            var shoppingList = FindPersonList(personListId);
            if (shoppingList == null)
            {
                return NotFound($"Shopping list by id {personListId} was not found");
            }

            shoppingList.Person.Add(person);

            return Ok();
        }

        // add pet to existing person
        [HttpPut("{personListId}/{personId}")]
        public IActionResult UpdatePersonPetList(int personListId, int personId, Pet pet)
        {
            var oldPersonList = FindPersonList(personListId);
            if (oldPersonList == null)
            {
                return NotFound($"Shopping list by id {personListId} was not found");
            }

            var oldPerson = FindPerson(personId, oldPersonList);
            if (oldPerson == null)
            {
                return NotFound($"Shopping list by id {personId} was not found");
            }

            oldPerson.Pets.Add(pet);
            
            return Ok(oldPersonList);
        }

        private PersonList FindPersonList(int id)
        {
            foreach (var list in _personList)
            {
                if (list.Id == id)
                {
                    return list;
                }
            }

            return null;
        }

        private Person FindPerson(int id, PersonList oldPersonList)
        {
            //foreach (var person in oldPersonList)
            //{
            //    if (person. == id)
            //    {
            //        return person;
            //    }
            //}

            for (int i = 0; i < _personList.Count; i++)
            {
                if (oldPersonList.Person[i].Id == id)
                {
                    return oldPersonList.Person[i];
                }
            }

            return null;
        }

    }
}
