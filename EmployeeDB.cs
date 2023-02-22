using System;
using System.Collections.Generic;
using System.IO;

namespace EmpDB
{
    internal class EmployeeDB
    {
        // global boolean switch for all test code
        public const bool _DEBUG_TEST_ = true;

        // storage container while the payroll app is running
        private List<Employee> employees = new List<Employee>();

        // input file to read employees fro
        private const string EMPLOYEE_DATA_INPUTFILE = "EMPLOYEE_DATA_INPUTFILE.txt";


        public EmployeeDB()
        {
            if (_DEBUG_TEST_) TestMain();
/*            ReadDataFromInputFile();*/
        }

        // function: displays the main menu of options for
        //           employee CRUD operations
        // preconditions: The program has been executed
        // input: none
        // output: menu options are displayed
        // postconditions: the user knows what keys to enter for the GoPayroll method
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

        // function:main user interface for the paroll app
        // preconditions: The program has successfully executed and is waiting for user input
        // input: single character (A, E, D, F, P, Q)
        // output: method execution as dictated by input
        // postconditions: user is either in a submenu or has exited the program
        internal void GoPayroll()
        {
            Console.Clear();
            string email = string.Empty;

            // displays the main menu until either a valid option is selected
            // or the user exits the program.
            while (true)
            {
                // displays the main menu
                DisplayMainMenu();

                // capture the user's choice
                char choice = GetUserInputChar();

                // execute the user's choice 
                // CRUD operations - Add, Edit, Delete, Find/Print
                switch (choice)
                {
                    case 'A':
                    case 'a':
                        //AddNewEmployee();
                        break;
                    case 'E':
                    case 'e':
                        EditEmployeeRecord();
                        break;
                    case 'D':
                    case 'd':
                        DeleteEmployee();
                        break;
                    case 'F':
                    case 'f':
                        FindEmployeeRecord(out email);
                        break;
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'Q':
                    case 'q':
                        QuitDatabaseAppAndSave();
                        break;
                    default:
                        break;
                }

            }
        }




        // function: allows teh user to delete a current employee's record
        // precondition: the employee must exist
        // input: either 'y' for yes or 'n' for no
        // output: Confirmation message that employee record is deleted
        // postcondition: record tied to the given email is deleted
        private void DeleteEmployee()
        {
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);
            Console.WriteLine($"{emp}");
            bool validChoice = false;
            if (emp != null)
            {
                while (!(validChoice))
                {
                    Console.WriteLine("Are you sure you want to delete this record? [Y]es/[N]o ");
                    char choice = GetUserInputChar();
                    switch (choice)
                    {
                        case 'Y':
                        case 'y':
                            validChoice = true;
                            employees.Remove(emp);
                            Console.WriteLine($"\nSystem has deleted the record associated with Social Securty Number: {email}");
                            break;
                        case 'N':
                        case 'n':
                            Console.Clear();
                            GoPayroll();
                            validChoice = true;
                            break;
                        default:
                            Console.Write("\nOnly ENTER 'Y' for Yes, or 'N' for No: ");
                            break;
                    }
                }
            }
        }

        // allows for recursive modification of employee record without displaying their
        // record twice in a row.
        private bool FromMain { get; set; } = false;

        // function: allows the user to edit a current employee's record by ENTERing their
        //           email address.
        // precondition: the user must choose the correct key as indicated by the main menu
        // input: a single character
        // output: the value of that single character
        // postcondition: the KeyCode has been returnd to the calling method
        private void EditEmployeeRecord()
        {
            FromMain = true;
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);

            if (emp != null)
            {
                // Employee exists, so their record can be edited
                EditEmployee(emp);
            }
            else
            {
                // employee does not exist and user is returned to the main menu
                Console.WriteLine($"\n************* RECORD NOT FOUND ************* " +
                    $"\n Can't edit record for user with email: {email}");
            }
        }

        // function: allows the user to edit the properties of an employee record
        // precondition: the employee must exist
        // input: single characters are ENTERed for submenu options, while
        //        both strings and deciamls are required for the properties,
        //        depending on which option the user chooses
        // output: the updated employee record is displayed
        // postcondition: the employee's record has been updated
        private void EditEmployee(Employee emp)
        {
            string employeeType = emp.GetType().Name;
            if (FromMain)
            {
                Console.Write(emp);
            }
            Console.WriteLine($"\nEditing record of a: {employeeType}");
            EditEmployeeMenu();
            char choice = GetUserInputChar();

            if (employeeType == "SalariedEmployee")
            {
                SalariedEmployee salaried = emp as SalariedEmployee;
                switch (choice)
                {
                    case 'S':
                    case 's':
                        Console.Write("\nENTER new weekly salary: ");
                        string salary = Console.ReadLine();
                        salaried.WeeklySalary = salaried.ValidateDecimal(salary);
                        break;
                    // these cases notify user that they chose an invalid option for
                    // a Salaried employee
                    case 'W':
                    case 'w':
                    case 'H':
                    case 'h':
                    case 'G':
                    case 'g':
                    case 'C':
                    case 'c':
                    case 'B':
                    case 'b':
                        Console.WriteLine(@"
        ***********************************************
        ***********************************************
        ***********************************************
        ***                                         ***
        ***      Only ENTER valid options for:      ***
        ***           Salaried Employess            ***
        ***                                         ***
        ***********************************************
        ***********************************************
        ***********************************************
        ");
                        break;
                }
            }
            else if (employeeType == "HourlyEmployee")
            {
                HourlyEmployee hourlyEmployee = emp as HourlyEmployee;
                switch (choice)
                {
                    case 'W':
                    case 'w':
                        Console.Write("\nENTER new hourly wage: ");
                        string wage = Console.ReadLine();
                        hourlyEmployee.Wage = hourlyEmployee.ValidateDecimal(wage);
                        break;
                    case 'H':
                    case 'h':
                        Console.Write("\nENTER new hours worked: ");
                        string hours = Console.ReadLine();
                        hourlyEmployee.Wage = hourlyEmployee.ValidateDecimal(hours); 
                        break;
                    // these cases notify user that they chose an invalid option for
                    // a Salaried employee
                    case 'S':
                    case 's':
                    case 'G':
                    case 'g':
                    case 'C':
                    case 'c':
                    case 'B':
                    case 'b':
                        Console.WriteLine(@"
        ***********************************************
        ***********************************************
        ***********************************************
        ***                                         ***
        ***      Only ENTER valid options for:      ***
        ***            Hourly Employess             ***
        ***                                         ***
        ***********************************************
        ***********************************************
        ***********************************************
        ");
                        break;
                }
            }
            else if (employeeType == "CommissionEmployee")
            {
                CommissionEmployee commisionEmployee = emp as CommissionEmployee;
                switch (choice)
                {
                    case 'G':
                    case 'g':
                        Console.Write("\nENTER new gross sales: ");
                        string sales = Console.ReadLine();
                        commisionEmployee.GrossSales = commisionEmployee.ValidateDecimal(sales);
                        break;
                    case 'C':
                    case 'c':
                        Console.Write("\nENTER new commission rate: ");
                        string rate = Console.ReadLine();
                        commisionEmployee.CommissionRate = commisionEmployee.ValidateDecimal(rate);
                        break;
                    // these cases notify user that they chose an invalid option for
                    // a Salaried employee
                    case 'S':
                    case 's':
                    case 'W':
                    case 'w':
                    case 'H':
                    case 'h':
                    case 'B':
                    case 'b':
                        Console.WriteLine(@"
        ***********************************************
        ***********************************************
        ***********************************************
        ***                                         ***
        ***      Only ENTER valid options for:      ***
        ***          Commission Employess           ***
        ***                                         ***
        ***********************************************
        ***********************************************
        ***********************************************
        ");
                        break;
                }
            }
            else if (employeeType == "BasePlusCommissionEmployee")
            {
                BasePlusCommissionEmployee basePlusCommisionEmployee = emp as BasePlusCommissionEmployee;
                switch (choice)
                {
                    case 'B':
                    case 'b':
                        Console.Write("\nENTER new base salary: ");
                        string salary = Console.ReadLine();
                        basePlusCommisionEmployee.GrossSales = basePlusCommisionEmployee.ValidateDecimal(salary);
                        break;
                    // these cases notify user that they chose an invalid option for
                    // a Salaried employee
                    case 'S':
                    case 's':
                    case 'W':
                    case 'w':
                    case 'H':
                    case 'h':
                    case 'G':
                    case 'g':
                    case 'C':
                    case 'c':
                        Console.WriteLine(@"
        ***********************************************
        ***********************************************
        ***********************************************
        ***                                         ***
        ***      Only ENTER valid options for:      ***
        ***     Base Plus Commission Employess      ***
        ***                                         ***
        ***********************************************
        ***********************************************
        ***********************************************
        ");
                        break;
                }
            }
            switch (choice)
            {
                case 'F':
                case 'f':
                    Console.Write("\nENTER new first name: ");
                    emp.FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("\nENTER new last name: ");
                    emp.LastName = Console.ReadLine();
                    break;
                // gives user option to quit to main menu
                case 'X':
                case 'x':
                case 'Q':
                case 'q':
                    Console.Clear();
                    GoPayroll();
                    break;
                /////////////// case '`':
                //////////////     BackdoorAccess();
                /////////////      break;
                default:

                    break;
            }
            Console.WriteLine($"\nEDIT operation done. Current record info:\n{emp}\nPress any key to continue.");
            Console.ReadKey();
            FromMain = false;
            EditEmployee(emp);
        }

        // function: Validates user input as a deicmal and reprompts them if it is not.
        // precondition: 
        // input: a singl
        // output: the va
        // postcondition:
        /*private decimal ValidateDecimal(string number)
        {
            if (!(decimal.TryParse(number, out decimal result)))
            {
                Console.Write("Please ENTER decimal values ONLY: ");
                number = Console.ReadLine();
                result = ValidateDecimal(number);
            }
            return result;
        }*/

        private void EditEmployeeMenu()
        {
            Console.Write(@"
        *******************************************************************
        ********************** Edit Employee Menu *************************
        *******************************************************************
        [F]irst name
        [L}ast name
        Weekly [S]alary                 (Salaried Employees only)
        [W]age                          (Hourly Employees only)
        [H]ours                         (Hourly Employees only)
        [G]ross Sales                   (Commission Employees only)
        [C]ommision Rate                (Commission Employees only)
        [B]ase Salary                   (Base Plus Commission Employees only)
        [Q]uit to main menu
        **Email address can never be changed. See admin.
        ENTER menu selection: ");
        }

        // function: returns the KeyCode for a user's input
        // precondition: the user has been prompted for a single character input
        // input: a single character
        // output: the value of that single character
        // postcondition: the KeyCode has been returnd to the calling method
        private char GetUserInputChar()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }



        // searches the list of employees for an employee
        // with a matching Email address. If found,
        // return the employee else null
        private Employee FindEmployeeRecord(out string email)
        {
            Console.WriteLine("\nENTER the email address of the employee to search: ");
            email = Console.ReadLine();

            foreach (var emp in employees)
            {
                if (email == emp.EmailAddress)
                {
                    Console.WriteLine($"FOUND email address: {emp.EmailAddress}");
                    return emp;
                }
            }
            // no matching email address
            Console.WriteLine($"{email} NOT FOUND!");
            return null;
        }

        // function: prints all existing employee records
        // precondition: the client has selected the menu option 'P' to print all employee records
        // input: none
        // output: none
        // postcondition: a printout of all existing employee records
        private void PrintAllRecords()
        {
            foreach(var emp in employees)
            {
                Console.WriteLine(emp);
            }
        }

        // function: reads the input file of employee records and stores them in a list
        // precondition: the client has run the program and data is expected to be read instantly
        // input: none
        // output: none
        // postcondition: a local list stores each employee record from the file
        private void ReadDataFromInputFile()
        {
            // 1 - make the file object and connect to the file on disk.
            StreamReader inFile = new StreamReader(EMPLOYEE_DATA_INPUTFILE);

            // 2 - use the file
            string employeeType = string.Empty;
            while ((employeeType = inFile.ReadLine()) != null)
            {
                // gather the essential information from the employee
                string firstName = inFile.ReadLine();
                string lastName = inFile.ReadLine();
                string emailAdress = inFile.ReadLine();
                string socialSecurityNumber = inFile.ReadLine();

                if (employeeType == "HourlyEmployee")
                {
                    // get wage and hours worked
                    decimal hourlyWage = decimal.Parse(inFile.ReadLine());
                    decimal hoursWorked = decimal.Parse(inFile.ReadLine());
                    

                    // create HourlyEmployee object and store into list
                    HourlyEmployee employee = new HourlyEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                        hourlyWage, hoursWorked);
                    employees.Add(employee);
                }
                else if (employeeType == "SalariedEmployee")
                {
                    // get weekly salary
                    decimal weeklySalary = decimal.Parse(inFile.ReadLine());

                    // create HourlyEmployee object and store into list
                    SalariedEmployee employee = new SalariedEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                        weeklySalary);
                    employees.Add(employee);
                }
                else if(employeeType == "CommissionEmployee")
                {
                    // get gross weekly sales and commission rate
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());

                    // create CommissionEmployee object and store into list
                    CommissionEmployee employee = new CommissionEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                        grossSales, commissionRate);
                    employees.Add(employee);
                }

                else if(employeeType == "BasePlusCommissionEmployee")
                {
                    // get gross weekly sales, commission rate, and base salary
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());
                    decimal baseSalary = decimal.Parse(inFile.ReadLine());

                    // create HourlyEmployee object and store into list
                    BasePlusCommissionEmployee employee = new BasePlusCommissionEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                        grossSales, commissionRate, baseSalary);
                    employees.Add(employee);
                }
            }
            // 3 - Close the resource - once, when all reading is done
            inFile.Close();
        }


        private void QuitDatabaseAppAndSave()
        {
            // 1 - make the file object and connect it to an actual file on disk
            StreamWriter saveFile = new StreamWriter("_EMPLOYEE_SAVE_FILE_.txt");

            // 2 - use the object
            Console.WriteLine("\n***** Current contents of Employee Database *****");
            foreach (Employee stu in employees)
            {
                // for now, echo the output to the shell for testing
                Console.WriteLine(stu.ToStringForSaveFile());
                saveFile.WriteLine(stu.ToStringForSaveFile());
            }

            // 3 - close the file object
            saveFile.Close();
            Environment.Exit(0);
        }

        public void TestMain()
        {
            // Read employees from input file
            ReadDataFromInputFile();
/*            // make 4 employee objects
            Employee emp1 = new SalariedEmployee("Albert:", "Albertson", "aalberston@job.com", "111111111", 3500.00m);
            Employee emp2 = new HourlyEmployee("Bonnie", "Bosch", "bbosch@job.com", "222222222", 15.45m, 40.00m);
            Employee emp3 = new CommissionEmployee("Charlie", "Cook", "ccook@job.com", "333333333", 7536.45m, 0.07m);
            Employee emp4 = new BasePlusCommissionEmployee("David", "Derelict", "dderelict@job.com", "444444444", 50097.05m, 0.05m, 3000m);


            // add the 4 objects to the list
            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(emp3);
            employees.Add(emp4);

            // make an anonymous object and put it in list
            employees.Add(new SalariedEmployee("Edgar", "Evengard", "eevengard@job.com", "555555555", 1300m));
            employees.Add(new HourlyEmployee("Fred", "Flinstone", "fflinstone@job.com", "666666666", 17.35m, 50m));
            employees.Add(new CommissionEmployee("Gina", "Gladstone", "ggladstone@job.com", "777777777", 7500m, 0.08m));
            employees.Add(new BasePlusCommissionEmployee("Howard", "Henderson", "hhenderson@job.com", "888888888", 7600m, .07m, 3400m));*/


            //Console.WriteLine(stu1);
            //Console.WriteLine(stu2);
            //Console.WriteLine(stu3);
        }
    }
}