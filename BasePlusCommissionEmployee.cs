using System;

namespace EmpDB
{
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary; // base salary per week

        // six-parameter constructor
        public BasePlusCommissionEmployee(string firstName, string lastName,
           string email,string ssn, decimal grossSales,
           decimal commissionRate, decimal baseSalary)
           : base(firstName, lastName, email,ssn,
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
                    Console.Write("ENTER a valid value for base salary: ");
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

        // return string representation of BasePlusCommissionEmployee
        public override string ToString() =>
           $"base-salaried {base.ToString()}\nbase salary: {BaseSalary:C}";

        public override string ToStringForSaveFile()
        {
            return base.ToStringForSaveFile() + $"\n{BaseSalary}";
        }
    }
}