using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class ShoppingCart
    {
        private readonly SqlDbContext _sqlDbContext;

        public ShoppingCart(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppinCartItem> ShoppinCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<SqlDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Drink drink, int amount)
        {
            var shoppingCartItem = _sqlDbContext.ShoppinCartItems.SingleOrDefault(
                s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppinCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Drink = drink,
                    Amount = 1
                };

                _sqlDbContext.ShoppinCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _sqlDbContext.SaveChanges();
        }

        public int RemoveFromCart(Drink drink)
        {
            var shoppingCartItem = _sqlDbContext.ShoppinCartItems.SingleOrDefault(
                s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _sqlDbContext.ShoppinCartItems.Remove(shoppingCartItem);
                }                
            }

            _sqlDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppinCartItem> GetShoppinCartItems()
        {
            return ShoppinCartItems ??
                (ShoppinCartItems = _sqlDbContext.ShoppinCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Include(s => s.Drink)
                    .ToList());                    
        }

        public void ClearCart()
        {
            var cartItems = _sqlDbContext.ShoppinCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _sqlDbContext.ShoppinCartItems.RemoveRange(cartItems);

            _sqlDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _sqlDbContext.ShoppinCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Drink.Price * c.Amount).Sum();
            return total;
        }
    }
}
