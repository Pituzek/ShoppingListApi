using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Extensions
{
    public static class IEnumerableExtension
    {
        public static string ToStringMany<T>(this IEnumerable<T> items)
        {
            if (items == null || items?.Any() == false) return "";
            // item1, item2
            return string.Join(
                ", ",
                items.Select(item => item.ToString())
            ) + ".";
        }
    }
}
