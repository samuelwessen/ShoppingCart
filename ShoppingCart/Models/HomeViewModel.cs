using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Drink> PreferredDrink { get; set; }
    }
}
