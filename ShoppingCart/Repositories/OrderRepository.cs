using ShoppingCart.Data;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SqlDbContext _sqlDbContext;
        private readonly Models.ShoppingCart _shoppingCart;

        public OrderRepository(SqlDbContext sqlDbContext, Models.ShoppingCart shoppingCart)
        {
            _sqlDbContext = sqlDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _sqlDbContext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppinCartItems;

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    DrinkId = item.Drink.DrinkId,
                    OrderId = order.OrderId,
                    Price = item.Drink.Price
                };
                _sqlDbContext.OrderDetails.Add(orderDetail);
            }
            _sqlDbContext.SaveChanges();
        }
    }
}
