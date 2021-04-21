using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDrinkRepository _drinkRepository;

        public HomeController(ILogger<HomeController> logger, IDrinkRepository drinkRepository)
        {
            _logger = logger;
            _drinkRepository = drinkRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredDrink = _drinkRepository.PreferredDrinks
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
