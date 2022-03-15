using FluentAssertions;
using NRules;
using NRules.Fluent;
using Payment.RuleEngine.Domain;
using Payment.RuleEngine.Rules;
using Xunit;

namespace Payment.RuleEngine.Tests
{
    public class RuleEngineTests
    {
        [Fact]
        public void Preferred_Customer_Rule_Test()
        {
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(DiscountNotificationRule1).Assembly));

            //Compile rules
            var factory = repository.Compile();

            //Create a working session
            var session = factory.CreateSession();

            //Load domain model
            var customer = new Customer("John Doe") { IsPreferred = true };
            var order1 = new Order(123456, customer, 2, 25.0);
            var order2 = new Order(123457, customer, 1, 100.0);

            //Insert facts into rules engine's memory
            session.Insert(customer);
            session.Insert(order1);
            session.Insert(order2);

            //Start match/resolve/act cycle
            session.Fire();


            order1.PercentDiscount.Should().Be(10);
            order2.PercentDiscount.Should().Be(10);
            customer.Notifications.Count.Should().Be(4);

        }
    }
}
