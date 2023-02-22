using System;

namespace EmpDB
{
    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary;

        // constructor using First/Last name,email, and Weekly salary 
        public SalariedEmployee(string firstName, string lastName,
           string email, string ssn, decimal weeklySalary)
           : base(firstName, lastName, email, ssn)
        {
            WeeklySalary = weeklySalary;
        }

        // property that gets and sets salary while validating that salaray is more than $0
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    Console.WriteLine("Weekly salary cannot be less than $0");
                    Console.Write("ENTER a valid salary: ");
                    string salary = Console.ReadLine();
                    WeeklySalary = ValidateDecimal(salary);
                }
                else
                {
                    weeklySalary = value;
                }
            }
        }

        // calculates earnings by overriding the abstract method declared in the Employee base class
        public override decimal Earnings() => WeeklySalary;

        // return string representation of SalariedEmployee object
        public override string ToString() =>
           $"Salaried employee: {base.ToString()}\n" +
           $"Weekly salary: {WeeklySalary:C}";
        
        public override string ToStringForSaveFile()
        {
            string str = GetType().Name + "\n";
            return str + base.ToStringForSaveFile() + $"{WeeklySalary}";
        }
    }


}