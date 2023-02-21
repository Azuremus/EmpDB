using System;

namespace EmpDB
{
    public class HourlyEmployee : Employee
    {
        private decimal wage; // wage per hour
        private decimal hours; // hours worked for the week

        // five-parameter constructor
        public HourlyEmployee(string firstName, string lastName,
           string email, decimal hourlyWage,
           decimal hoursWorked)
           : base(firstName, lastName, email)
        {
            Wage = hourlyWage; // validate hourly wage
            Hours = hoursWorked; // validate hours worked
        }

        // property that gets and sets hourly employee's wage
        public decimal Wage
        {
            get
            {
                return wage;
            }
            set
            {
                if (value < 0) // validation
                {
                    Console.WriteLine("Hourly wage cannot be less than $0");
                    Console.Write("ENTER a valid hourly wage: ");
                    string hourlyWage = Console.ReadLine();
                    Wage = ValidateDecimal(hourlyWage);
                }
                else
                {
                    wage = value;
                }
            }
        }

        // property that gets and sets hourly employee's hours
        public decimal Hours
        {
            get
            {
                return hours;
            }
            set
            {
                if (value < 0 || value > 168) // validation
                {
                    Console.WriteLine("Hours worked must be >= 0 and <= 168");
                    Console.Write("ENTER a valid value for hours: ");
                    string hoursWorked = Console.ReadLine();
                    Hours = ValidateDecimal(hoursWorked);
                }
                else
                {
                    hours = value;
                }
            }
        }

        // calculate earnings; override Employee's abstract method Earnings
        public override decimal Earnings()
        {
            if (Hours <= 40) // no overtime
            {
                return Wage * Hours;
            }
            else
            {
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5M);
            }
        }

        // return string representation of HourlyEmployee object
        public override string ToString() =>
           $"hourly employee: {base.ToString()}\n" +
           $"hourly wage: {Wage:C}\nhours worked: {Hours:F2}";
    }
}