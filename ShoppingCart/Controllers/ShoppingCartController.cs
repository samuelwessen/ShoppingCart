using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly Models.ShoppingCart _shoppingCart;

        public ShoppingCartController(IDrinkRepository drinkRepository, Models.ShoppingCart shoppingCart)
        {
            _drinkRepository = drinkRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppinCartItems();
            _shoppingCart.ShoppinCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(sCVM);
        }

        public RedirectToActionResult AddToShoppingCart(int drinkId)
        {
            var selecteddrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);

            if(selecteddrink != null)
            {
                _shoppingCart.AddToCart(selecteddrink, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int drinkId)
        {
            var selecteddrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);

            if(selecteddrink != null)
            {
                _shoppingCart.RemoveFromCart(selecteddrink);
            }

            return RedirectToAction("Index");
        }
    }
}
