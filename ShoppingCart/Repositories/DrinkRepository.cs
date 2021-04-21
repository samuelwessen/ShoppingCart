using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly SqlDbContext _sqlDbContext;

        public DrinkRepository(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }


        public IEnumerable<Drink> Drinks => _sqlDbContext.Drinks.Include(c => c.Category);

        public IEnumerable<Drink> PreferredDrinks => _sqlDbContext.Drinks.Where(p => p.IsPreferredDrink).Include(c => c.Category);

        public Drink GetDrinkById(int drinkId)
        {
            return _sqlDbContext.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
        }
    }
}
