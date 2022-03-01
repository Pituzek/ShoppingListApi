using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi
{
    public class PersonList
    {
        public int Id { get; set; }
        public List<Person> Person { get; set; } = new List<Person>();
    }
}