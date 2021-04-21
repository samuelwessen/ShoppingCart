using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Models.ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, Models.ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
