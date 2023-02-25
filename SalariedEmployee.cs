// (C)Copyright 1992 - 2017 by Deitel & Associates, Inc. and               
// Pearson Education, Inc. All Rights Reserved.        
using System;

namespace EmpDB
{
    // Fig. 12.5: SalariedEmployee.cs
    // SalariedEmployee class that extends Employee.
    public class SalariedEmployee : Employee, IPayable
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
                    Console.Write($"ENTER a valid salary for {FirstName} {LastName}: ");
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
        
        public decimal GetPaymentAmount() => Earnings();
       

        // return string representation of SalariedEmployee object
        public override string ToString() =>
           $"{"Salaried employee: ",35}{base.ToString()}\n" +
           $"{"Weekly salary: ",35}{WeeklySalary:C}";
        
        public override string ToStringForSaveFile()
        {
            string str = GetType().Name + "\n";
            return str + base.ToStringForSaveFile() + $"{WeeklySalary}";
        }
    }


}