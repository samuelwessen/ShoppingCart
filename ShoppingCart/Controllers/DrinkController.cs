using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class DrinkController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;

        public DrinkController(ICategoryRepository categoryRepository, IDrinkRepository drinkRepository)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository;
        }

        public ViewResult Index()
        {
            DrinkListViewModel vm = new DrinkListViewModel();
            vm.Drinks = _drinkRepository.Drinks;
            vm.CurrentCategory = "DrinkCategory";

            
            return View(vm);
        }
    }
}
