using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Db
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
        public bool IsProgressiveTaxes { get; set; }
        public decimal? FixedTaxes { get; set; }
    }
}
