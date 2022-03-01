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
                return NotFound($"People list by id {personListId} was not found");
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
                return NotFound($"People list by id {personListId} was not found");
            }

            var oldPerson = FindPerson(personId, oldPersonList);
            if (oldPerson == null)
            {
                return NotFound($"Person of id {personId} was not found");
            }

            oldPerson.Pets.Add(pet);
            
            return Ok(oldPersonList);
        }

        // Remove Person from a list
        [HttpDelete("{personListId}/{personId}")]
        public IActionResult DeletePersonFromList(int personListId, int personId)
        {
            var oldPersonList = FindPersonList(personListId);
            if (oldPersonList == null)
            {
                return NotFound($"People list by id {personListId} was not found");
            }

            var oldPerson = FindPerson(personId, oldPersonList);
            if (oldPerson == null)
            {
                return NotFound($"Person of id {personId} was not found");
            }

            oldPersonList.Person.Remove(oldPerson);
            return Ok(oldPersonList);
        }

        // Remove Pet from existing Person
        [HttpDelete("{personListId}/{personId}/{petID}")]
        public IActionResult DeletePersonPetFromList(int personListId, int personId, int petId)
        {
            var oldPersonList = FindPersonList(personListId);
            if (oldPersonList == null)
            {
                return NotFound($"People list by id {personListId} was not found");
            }

            var oldPerson = FindPerson(personId, oldPersonList);
            if (oldPerson == null)
            {
                return NotFound($"Person of id {personId} was not found");
            }

            var oldPet = FindPet(petId, oldPerson);
            if (oldPet == null)
            {
                return NotFound($"Pet of id {petId} was not found");
            }

            oldPerson.Pets.Remove(oldPet);
            return Ok(oldPersonList);
        }

        // update existing person data
        [HttpPatch("updatePersonData/{personListId}/{personId}")]
        public IActionResult UpdateExistingPersonData(int personListId, int personId, Person person)
        {
            var oldPersonList = FindPersonList(personListId);
            if (oldPersonList == null)
            {
                return NotFound($"People list by id {personListId} was not found");
            }

            var oldPerson = FindPerson(personId, oldPersonList);
            if (oldPerson == null)
            {
                return NotFound($"Person of id {personId} was not found");
            }

            oldPerson.Name = person.Name;
            oldPerson.Age = person.Age;
            if (person.Pets.Count !=0) oldPerson.Pets = person.Pets; // do edycji danych zwierzecia, stworzylbym osobna funkcje
            oldPerson.SpouseID = person.SpouseID;

            return Ok(oldPersonList);
        }

        // marry two people
        [HttpPatch("{personListId}/merryTwoPeople/{idOfFirstPerson}/{idOfSecondPerson}")]
        public IActionResult MerryTwoPeople(int personListId, int idOfFirstPerson, int idOfSecondPerson)
        {
            var oldPersonList = FindPersonList(personListId);
            if (oldPersonList == null)
            {
                return NotFound($"People list by id {personListId} was not found");
            }

            var firstPerson = FindPerson(idOfFirstPerson, oldPersonList);
            if (firstPerson == null)
            {
                return NotFound($"Person of id {idOfFirstPerson} was not found");
            }

            var secondPerson = FindPerson(idOfSecondPerson, oldPersonList);
            if (firstPerson == null)
            {
                return NotFound($"Person of id {idOfSecondPerson} was not found");
            }

            Marry(firstPerson, secondPerson);

            return Ok(oldPersonList);
        }

        private static void Marry(Person firstPerson, Person secondPerson)
        {
            firstPerson.SpouseID = secondPerson.Id;
            secondPerson.SpouseID = firstPerson.Id;
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
            foreach (Person person in oldPersonList.Person)
            {
                if (person.Id == id)
                {
                    return person;
                }
            }
            return null;
        }

        private Pet FindPet(int id, Person person)
        {
            foreach(Pet pet in person.Pets)
            {
                if (pet.Id == id)
                {
                    return pet;
                }
            }
            return null;
        }
    }
}
