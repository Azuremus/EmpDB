using System;

namespace EmpDB
{
    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; }

        // three-parameter constructor
        public Employee(string firstName, string lastName,
           string email)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
        }

        // return string representation of Employee object, using properties
        public override string ToString() => $"{FirstName} {LastName}\n" +
           $"Email address: {EmailAddress}";

        // abstract method overridden by derived classes
        public abstract decimal Earnings(); // no implementation here

        // adds validator for all decimal properties without throwing exceptions
        public decimal ValidateDecimal(string number)
        {
            if (!(decimal.TryParse(number, out decimal result)))
            {
                Console.Write("Please ENTER decimal values ONLY: ");
                number = Console.ReadLine();
                result = ValidateDecimal(number);
            }
            return result;
        }
    }
}