using System.Collections.Generic;
using System.Linq;
using NRules.Fluent.Dsl;
using NRules.RuleModel;
using Payment.RuleEngine.Domain;

namespace Payment.RuleEngine.Rules
{
    //[Priority(1)]
    public class DiscountNotificationRule1 : Rule
    {
        public override void Define()
        {
            Customer customer = default;

            When()
                .Match<Customer>(() => customer)
                .Exists<Order>(o => o.Customer == customer, o => o.PercentDiscount > 0.0 ,o=>o.Customer.Notifications.Count==0);

            Then()
                .Do(_ => customer.NotifyAboutDiscount($"Enjoy the Discount"));
            //.Do(ctx => ctx.Retract(customer))
            ;

        }
    }

    //[Priority(10)]
    public class DiscountNotificationRule2 : Rule
    {
        public override void Define()
        {
            Customer customer = default;

            When()
                .Match<Customer>(() => customer)
                .Exists<Order>(o => o.Customer == customer, o => o.PercentDiscount > 0.0, o => o.Customer.Notifications.Count == 0);

            Then()
                .Do(_ => customer.NotifyAboutDiscount($"You got Discount"));
            //.Do(ctx => ctx.Retract(customer));


        }
    }

    
    public class OrderDiscountNotificationRule : Rule
    {
        public override void Define()
        {
            Customer customer = default;
            IEnumerable<Order> orders = default;

            When()
                .Match<Customer>(() => customer)
                .Exists<Order>(o => o.Customer == customer, o => o.PercentDiscount > 0.0)
                .Query(() => orders, x => x
                    .Match<Order>(o => o.PercentDiscount > 0.0)
                    .Collect()
                    .Where(c => c.Any()))
                ;

            Then().Do(_ =>  SendNotificationForOrder(orders));
        }

        public static void SendNotificationForOrder(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Customer.NotifyAboutDiscount($"You got Discount for Order Id {order.Id}");
            }
        }
    }
}
