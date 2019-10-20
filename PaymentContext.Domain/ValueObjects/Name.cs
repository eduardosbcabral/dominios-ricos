using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if(string.IsNullOrEmpty(FirstName)) 
            {
                AddNotification("Name.FirstName", "Nome inválido.");
            }

            if(string.IsNullOrEmpty(LastName)) 
            {
                AddNotification("Name.LastName", "Sobrenome inválido.");
            }

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                .HasMinLen(LastName, 3, "Name.LastName", "Nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(LastName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres")
            );
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}