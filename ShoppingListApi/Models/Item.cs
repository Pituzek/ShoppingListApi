using ShoppingListApi.Extensions;

namespace ShoppingListApi
{
    public class Item
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set 
            { 
                // using extension method, to automatically capitalize first letter of a name
                _name = value.CapitalizeFirstLetter(); 
            }
        }
        public decimal Price { get; set; }
        public double Amount { get; set; }
    }
}
