///////////////////////////////////////////////////////
// TINFO 200 A, Winter 2023
// UWTacoma SET, Caleb Ghirmai and Ryan Enyeart-Youngblood
// 2023-03-04 - EmpDB - C# programming project - An employee payroll system
// A database for tracking payroll information for 4 different employee types.
// The program uses basic CRUD operations, with an additional Streamwriter
// function for storing information into a .txt file.

using System;
using System.Collections.Generic;

namespace EmpDB
{
    internal class EmployeeDB
    {

        // storage container while the payroll app is running
        private List<Employee> employees = new List<Employee>();
        internal void GoPayroll()
        {
            throw new NotImplementedException();
        }

        public void TestMain()
        {
            // make 3 student objects
            Employee emp1 = new SalariedEmployee("Albert:", "Albertson", "111111111",3500.00m);
            Employee emp2 = new HourlyEmployee("Bonnie","Bosch", "222222222", 15.45m, 40.00m);
            Employee emp3 = new CommissionEmployee("Charlie", "Cook", "333333333", 7536.45m, 0.07m);
            Employee emp4 = new BasePlusCommissionEmployee("David","Derelict", "444444444", 50097.05m, 0.05m,3000m);


            // add the 3 objects to the list
            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(emp3);
            employees.Add(emp4);

            // make an anonymous object and put it in list
            employees.Add(new SalariedEmployee("Edgar","Evengard", "555555555",1300m));
            employees.Add(new HourlyEmployee("Fred", "Flinstone", "666666666", 17.35m, 50m));
            employees.Add(new CommissionEmployee("Gina", "Gladstone", "777777777",7500m, 0.08m));
            employees.Add(new BasePlusCommissionEmployee("Howard", "Henderson","888888888", 7600m, .07m,3400m ));


            //Console.WriteLine(stu1);
            //Console.WriteLine(stu2);
            //Console.WriteLine(stu3);
        }
    }
}