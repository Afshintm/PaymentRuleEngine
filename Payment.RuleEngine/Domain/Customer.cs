using System.Collections.Generic;

namespace Payment.RuleEngine.Domain
{
    public class Customer
    {
        public string Name { get; }
        public bool IsPreferred { get; set; }
        public List<string> Notifications { get; set; }
        public Customer(string name)
        {
            Name = name;
            Notifications = new List<string>();
        }
        
        public void NotifyAboutDiscount(string message)
        {
            Notifications.Add(message);
        }
    }
}
