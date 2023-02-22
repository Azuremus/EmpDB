using System;

namespace EmpDB
{
    public class CommissionEmployee : Employee
    {
        private decimal grossSales; // gross weekly sales
        private decimal commissionRate; // commission percentage

        // five-parameter constructor
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
                    Console.Write("ENTER a valid value for gross sales: ");
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
                    Console.Write("ENTER a valid value for commission rate: ");
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

        // return string representation of CommissionEmployee object
        public override string ToString() =>
           $"commission employee: {base.ToString()}\n" +
           $"gross sales: {GrossSales:C}\n" +
           $"commission rate: {CommissionRate:F2}";

        public override string ToStringForSaveFile()
        {
            string str = GetType().Name + "\n";
            return str + base.ToStringForSaveFile() + $"{GrossSales}\n{CommissionRate}";
        }
    }
}