///////////////////////////////////////////////////////
// TINFO 200 A, Winter 2023
// UWTacoma SET, Caleb Ghirmai and Ryan Enyeart-Youngblood
// 2023-03-04 - EmpDB - C# programming project - An employee payroll system
// A database for tracking payroll information for 4 different employee types.
// The program uses basic CRUD operations, with an additional Streamwriter
// function for storing information into a .txt file.

using System;

namespace EmpDB
{
    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary;

        // constructor using First/Last name,SSN, and Weekly salary 
        public SalariedEmployee(string firstName, string lastName,
           string socialSecurityNumber, decimal weeklySalary)
           : base(firstName, lastName, socialSecurityNumber)
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
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(WeeklySalary)} cannot be less than $0");
                }
                weeklySalary = value;
            }
        }

        // calculates earnings by overriding the abstract method declared in base class, Employee
        public override decimal Earnings() => WeeklySalary;

        // return string representation of SalariedEmployee object
        public override string ToString() =>
           $"Salaried employee: {base.ToString()}\n" +
           $"Weekly salary: {WeeklySalary:C}";
    }


}