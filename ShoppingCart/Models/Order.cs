using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderDetail> OrderLines { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdressLine { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }



    }
}
