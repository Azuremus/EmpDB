﻿///////////////////////////////////////////////////////
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

        // displays the main menu of options for
        // employee CRUD operations
        private void DisplayMainMenu()
        {
            Console.WriteLine(@"
        ********************************************************
        ***************EMPLOYEE DATABASE MAIN MENU***************
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [A]dd new employee record
        [E]dit an existing employee record
        [D]elete an employee record
        [F]ind an employee in the database
        [P]rint out all employee records
        [Q]uit the app after saving
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        ");
        }

        // searches the list of employees for an employee
        // with a matching social security number. If found,
        // return the employee else null
        private Employee FindEmployeeRecord(out string socialSecurityNumber)
        {
            Console.WriteLine("\nENTER the social security number of the employee to search: ");
            socialSecurityNumber = Console.ReadLine();

            foreach (var emp in employees)
            {
                if (socialSecurityNumber == emp.SocialSecurityNumber)
                {
                    Console.WriteLine($"FOUND social security number: {emp.SocialSecurityNumber}");
                    return emp;
                }
            }
            // no matching social security number
            Console.WriteLine($"{socialSecurityNumber} NOT FOUND!");
            return null;
        }

        public void TestMain()
        {
            // make 3 student objects
            Employee emp1 = new SalariedEmployee();
            Employee emp2 = new HourlyEmployee();
            Employee emp3 = new CommisionEmployee();
            Employee emp4 = new BasePlusCommissionEmployee();


            // add the 3 objects to the list
            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(emp3);
            employees.Add(emp4);

            // make an anonymous object and put it in list
            employees.Add(new SalariedEmployee());
            employees.Add(new HourlyEmployee());
            employees.Add(new CommisionEmployee());
            employees.Add(new BasePlusCommissionEmployee());


            //Console.WriteLine(stu1);
            //Console.WriteLine(stu2);
            //Console.WriteLine(stu3);
        }
    }
}