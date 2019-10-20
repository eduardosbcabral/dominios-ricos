using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities 
{
    public class Student : Entity
    {
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray();
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public void AddSubscription(Subscription subscription) 
        {
            var hasSubscriptionActive = false;

            foreach(var sub in _subscriptions)
            {
                if(sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .IsGreaterThan(subscription.Payments.Count, 0, "Student.Subscription.Payments", "Esta assinatura não possui pagamentos")
            );

            _subscriptions.Add(subscription);

            // Alternativa ao modelo de contrato acima
            // if(hasSubscriptionActive)
            // {
            //     AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa");
            // }
        }
    }
}