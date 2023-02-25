// (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               
// Pearson Education, Inc. All Rights Reserved.                           
using System;

namespace EmpDB
{
    // Fig. 12.8: BasePlusCommissionEmployee.cs
    // BasePlusCommissionEmployee class that extends CommissionEmployee.using System;
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary; // base salary per week

        // six-parameter constructor
        public BasePlusCommissionEmployee(string firstName, string lastName,
           string email, string ssn, decimal grossSales,
           decimal commissionRate, decimal baseSalary)
           : base(firstName, lastName, email, ssn,
                grossSales, commissionRate)
        {
            BaseSalary = baseSalary; // validates base salary
        }

        // property that gets and sets
        // BasePlusCommissionEmployee's base salary
        public decimal BaseSalary
        {
            get
            {
                return baseSalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    Console.WriteLine("Base salary must be >= 0");
                    Console.Write($"ENTER a valid value for base salary for {FirstName} {LastName}: ");
                    string salary = Console.ReadLine();
                    BaseSalary = ValidateDecimal(salary);
                }
                else
                {
                    baseSalary = value;
                }
            }
        }

        // calculate earnings
        public override decimal Earnings() => BaseSalary + base.Earnings();

        public new decimal GetPaymentAmount() => Earnings();


        // return string representation of BasePlusCommissionEmployee
        public override string ToString() =>
           $"Base-salaried {base.ToString()}\n{"Base salary: ",35}{BaseSalary:C}";

        public override string ToStringForSaveFile()
        {
            return base.ToStringForSaveFile() + $"\n{BaseSalary}";
        }
    }
}