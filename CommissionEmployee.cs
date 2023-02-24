// (C) Copyright 1992-2017 by Deitel & Associates, Inc. and
// Pearson Education, Inc. All Rights Reserved.            
using System;
namespace EmpDB
{
    // Fig. 12.7: CommissionEmployee.cs
    // CommissionEmployee class that extends Employee.
    public class CommissionEmployee : Employee, IPayable
    {
        private decimal grossSales; // gross weekly sales
        private decimal commissionRate; // commission percentage

        // six-parameter constructor
        public CommissionEmployee(string firstName, string lastName,
           string email, string ssn, decimal grossSales,
           decimal commissionRate)
           : base(firstName, lastName, email, ssn)
        {
            GrossSales = grossSales; // validates gross sales
            CommissionRate = commissionRate; // validates commission rate
        }

        // property that gets and sets commission employee's gross sales
        public decimal GrossSales
        {
            get
            {
                return grossSales;
            }
            set
            {
                if (value < 0) // validation
                {
                    Console.WriteLine("Gross sales must be >= 0");
                    Console.Write($"ENTER a valid gross sales for {FirstName} {LastName}: ");
                    string sales = Console.ReadLine();
                    GrossSales = ValidateDecimal(sales);
                }
                else
                {
                    grossSales = value;
                }
            }
        }

        // property that gets and sets commission employee's commission rate
        public decimal CommissionRate
        {
            get
            {
                return commissionRate;
            }
            set
            {
                if (value <= 0 || value >= 1) // validation
                {
                    Console.WriteLine("Commission rate must be > 0 and < 1");
                    Console.Write($"ENTER a valid commission rate for {FirstName} {LastName}: ");
                    string rate = Console.ReadLine();
                    CommissionRate = ValidateDecimal(rate);
                }
                else
                {
                    commissionRate = value;
                }
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => CommissionRate * GrossSales;

        public decimal GetPaymentAmount() => Earnings();
        

        // return string representation of CommissionEmployee object
        public override string ToString() =>
           $"Commission employee: {base.ToString()}\n" +
           $"Gross sales: {GrossSales:C}\n" +
           $"Commission rate: {CommissionRate:F2}";

        public override string ToStringForSaveFile()
        {
            string str = GetType().Name + "\n";
            return str + base.ToStringForSaveFile() + $"{GrossSales}\n{CommissionRate}";
        }
    }
}