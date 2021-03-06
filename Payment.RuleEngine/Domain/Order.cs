using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.RuleEngine.Domain
{
    public class Order
    {
        public int Id { get; }
        public Customer Customer { get; }
        public int Quantity { get; }
        public double UnitPrice { get; }
        public double PercentDiscount { get; set; }
        public bool IsOpen { get; set; } = true;

        public Order(int id, Customer customer, int quantity, double unitPrice)
        {
            Id = id;
            Customer = customer;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
