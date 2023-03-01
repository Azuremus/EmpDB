using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;

namespace EmpDB
{
    internal class EmployeeDB
    {
        // global boolean switch for all test code
        public const bool _DEBUG_TEST_ = true;

        // storage container while the payroll app is running
        private List<IPayable> payables = new List<IPayable>();

        // input file to read payables from
        private const string EMPLOYEE_DATA_INPUTFILE = "_EMPLOYEE_SAVE_FILE_.txt";
        //"EMPLOYEE_DATA_INPUTFILE.txt";


        public EmployeeDB()
        {
            if (_DEBUG_TEST_)
            {
                TestMain();
                //ReadDataFromInputFile();
            }
        }

        // function: displays the main menu of options for
        //           employee CRUD operations
        // preconditions: The program has been executed
        // input: none
        // output: menu options are displayed
        // postconditions: the user knows what keys to enter for the GoPayroll method
        private void DisplayMainMenu()
        {
            Console.Write(@"
        ********************************************************
        ***************EMPLOYEE DATABASE MAIN MENU***************
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [A]dd new employee record
        [E]dit an existing employee record
        [D]elete an employee record
        [F]ind an employee in the database
        [S]ubmit new invoice
        [C]ancel invoice
        [V]iew invoice
        [P]rocess payroll 
        [Q]uit the app after saving
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Please ENTER your selection: ");
        }
        // [P]rint out all employee records => [P]rocess payroll


        // function:main user interface for the paroll app
        // preconditions: The program has successfully executed and is waiting for user input
        // input: single character (A, E, D, F, P, Q)
        // output: method execution as dictated by input
        // postconditions: user is either in a submenu or has exited the program
        internal void GoPayroll()
        {
            Console.Clear();
            string primaryKey = string.Empty;

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
                        AddNewEmployee();
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
                        FindEmployeeRecord(out primaryKey);
                        break;
                    case 'S':
                    case 's':
                        SubmitNewInvoice();
                        break;
                    case 'C':
                    case 'c':
                        CancelInvoice();
                        break;
                    case 'V':
                    case 'v':
                        ViewInvoice(out primaryKey);
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

        // function:
        // preconditions:
        // input:
        // output
        //postconditions:
        private void SubmitNewInvoice()
        {
            Console.WriteLine("\n************ SUBMIT NEW INVOICE ************");
            bool invoiceDuplicate = true;
            string invoiceNumber = string.Empty;
            List<Invoice> temp = new List<Invoice>();
            foreach (IPayable payable in payables)
            {
                if (payable.GetType().Name == "Invoice")
                {
                    temp.Add((Invoice)payable);
                }
            }
            while (invoiceDuplicate)
            {
                Console.Write("\nEnter the invoice number: ");
                invoiceNumber = Console.ReadLine();
                foreach (Invoice item in temp)
                {
                    if (item.InvoiceNumber == invoiceNumber)
                    {
                        Console.WriteLine("ERROR! That invoice number aleady exists!" +
                            "\nPlease check current records.");
                        invoiceDuplicate = true;
                    }
                    else
                    {
                        invoiceDuplicate = false;
                    }
                }
            }
            Console.Write("Enter the part number: ");
            string partNumber = Console.ReadLine();
            Console.Write("Enter the part description: ");
            string partDescription = Console.ReadLine();
            Invoice invoice = new Invoice(invoiceNumber, partNumber, partDescription, 0, 0);
            Console.Write("Enter the quantity ordered: ");
            string quantity = Console.ReadLine();
            invoice.Quantity = invoice.ValidateInteger(quantity);
            Console.Write("Enter the unit price: ");
            string unitPrice = Console.ReadLine();
            invoice.PricePerItem = invoice.ValidateDecimal(unitPrice);
            ConfirmNewInvoice(invoice);
        }

        // function:
        // preconditions:
        // input:
        // output
        //postconditions:
        private void ConfirmNewInvoice(Invoice invoice)
        {
            // print and confirm the new invoice
            // record with the user
            Console.WriteLine(invoice);
            Console.WriteLine("Submit this invoice to the database?");
            Console.Write("[Y]es, [C]ancel, [R]estart");

            char choice = GetUserInputChar();

            // user confirms the new invoice
            switch (choice)
            {
                case 'Y':
                case 'y':
                    Console.WriteLine("Invoice successfully added to database!");
                    payables.Add(invoice);
                    GoPayroll();
                    break;
                case 'C':
                case 'c':
                    // user wishes to cancel submission
                    GoPayroll();
                    break;
                case 'R':
                case 'r':
                    // clear console and call method to add new employee
                    Console.Clear();
                    Console.WriteLine("Previous entry: \n" + invoice.ToString());
                    SubmitNewInvoice();
                    break;
                default:
                    Console.WriteLine("\nPlese ENTER a valid choice (Y/C/R): ");
                    ConfirmNewInvoice(invoice);
                    break;
            }

        }

        // function:
        // preconditions:
        // input:
        // output
        //postconditions:
        private void CancelInvoice()
        {
            string invoiceNumber = string.Empty;
            Invoice invoice = ViewInvoice(out invoiceNumber);
            Console.WriteLine($"{invoice}");
            bool validChoice = false;
            if (invoice != null)
            {
                while (!(validChoice))
                {
                    Console.WriteLine("Are you sure you want to cancel this invoice? [Y]es/[N]o ");
                    char choice = GetUserInputChar();
                    switch (choice)
                    {
                        case 'Y':
                        case 'y':
                            validChoice = true;
                            payables.Remove(invoice);
                            Console.WriteLine($"\nSystem has deleted invoice: {invoiceNumber}");
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

        // function:
        // preconditions:
        // input:
        // output
        //postconditions:
        private Invoice ViewInvoice(out string primaryKey)
        {
            
            Console.Write("\nENTER the invoice number you would like to view: ");
            primaryKey = Console.ReadLine();
            List<Invoice> temp = new List<Invoice>();
            foreach (IPayable payable in payables)
            {
                if (payable.GetType().Name == "Invoice")
                {
                    temp.Add((Invoice)payable);
                }
            }
            foreach (Invoice invoice in temp)
            {
                if (primaryKey == invoice.InvoiceNumber)
                {
                    Console.WriteLine($"\n\nFOUND Invoice: {invoice.InvoiceNumber}\n");
                    Console.WriteLine(invoice);
                    
                    return invoice;
                }
            }
            // no matching email address
            Console.WriteLine($"{primaryKey} NOT FOUND!");
            return null;
        }

        // function: allows the user to delete a current employee's record
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
                            payables.Remove(emp);
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


        // function: allows the client to add a new employee record
        // precondition: the client has selected 'a' or 'A' at the menu
        // input:  none
        // output: none
        // postcondition: the new employee record is saved
        private void AddNewEmployee()
        {
            bool validChoice = false;
            Console.WriteLine("\n************ ADD NEW EMPLOYEE ************");
            Console.Write("\nEnter first name of employee: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name of employee: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter email of employee: ");
            string email = Console.ReadLine();
            Console.Write("Enter employee's social security number: ");
            string socialSecurityNumber = Console.ReadLine();
            while (validChoice == false)
            {
                Console.WriteLine("Select an employee type");
                Console.WriteLine(@"
        ********************************************************
        ***************Employee Type***************
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [H]ourly
        [S]alaried
        [C]ommission 
        [B]ase plus commission
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        ");

                char selection = GetUserInputChar();

                // process a new hourly employee
                if (char.ToUpper(selection) == 'H')
                {
                    validChoice = true;
                    // create a new HourlyEmployee object
                    HourlyEmployee emp = new HourlyEmployee(firstName, lastName, email, socialSecurityNumber, 0, 0);
                    Console.Write("\nEnter the employee's wage per hour: ");
                    string wage = Console.ReadLine();
                    emp.Wage = emp.ValidateDecimal(wage);
                    Console.Write("Enter employee's hours worked for the week: ");
                    string hoursWorked = Console.ReadLine();
                    emp.Hours = emp.ValidateDecimal(hoursWorked);

                    // Confirm new employee record with user
                    ConfirmAddEmployee(emp);
                }
                // process a new salaried employee
                else if (char.ToUpper(selection) == 'S')
                {
                    validChoice = true;
                    // create a new SalariedEmployee object
                    SalariedEmployee emp = new SalariedEmployee(firstName, lastName, email, socialSecurityNumber, 0);
                    Console.Write("\nEnter the employee's weekly salary: ");
                    string weeklySalary = Console.ReadLine();
                    emp.WeeklySalary = emp.ValidateDecimal(weeklySalary);

                    // Confirm new employee record with user
                    ConfirmAddEmployee(emp);
                }
                // process a new commission employee
                else if (char.ToUpper(selection) == 'C')
                {
                    validChoice = true;
                    // create a new CommissionEmployee object
                    CommissionEmployee emp = new CommissionEmployee(firstName, lastName, email, socialSecurityNumber, 0, 0);
                    Console.Write("\nEnter the employee's gross sales: ");
                    string grossSales = Console.ReadLine();
                    emp.GrossSales = emp.ValidateDecimal(grossSales);
                    Console.Write("\nEnter the employee's commission rate: ");
                    string commissionRate = Console.ReadLine();
                    emp.CommissionRate = emp.ValidateDecimal(commissionRate);

                    // Confirm new employee record with user
                    ConfirmAddEmployee(emp);
                }
                // process a new base plus commission employee
                else if (char.ToUpper(selection) == 'B')
                {
                    validChoice = true;
                    // create a new BasePlusCommissionEmployee object
                    BasePlusCommissionEmployee emp = new BasePlusCommissionEmployee(firstName,
                        lastName, email, socialSecurityNumber, 0, 0, 0);
                    Console.Write("\nEnter the employee's gross sales: ");
                    string grossSales = Console.ReadLine();
                    emp.GrossSales = emp.ValidateDecimal(grossSales);
                    Console.Write("Enter the employee's commission rate: ");
                    string commissionRate = Console.ReadLine();
                    emp.CommissionRate = emp.ValidateDecimal(commissionRate);
                    Console.Write("Enter the employee's base salary: ");
                    string baseSalary = Console.ReadLine();
                    emp.BaseSalary = emp.ValidateDecimal(baseSalary);

                    // Confirm new employee record with user
                    ConfirmAddEmployee(emp);
                }
                else
                {
                    Console.WriteLine("\nThat is an invalid option!\nPlease ENTER one of the following:" +
                        "\n\t[H]ourly\n\t[S]alaried\n\t[C]ommission\n\t[B]ase plus commission");
                }
            }
        }

        // function: allows the user to confirm a new employee condition
        // precondition: the client has attempted to add a new employee record
        //               and they have entered the necessary details for the employee type
        //
        // input:  Employee emp
        // output: none
        // postcondition: the employee has confirmed or rejected the addition of a new
        //                employee object
        //
        private void ConfirmAddEmployee(Employee emp)
        {
            // print and confirm the new employee
            // record with the user
            Console.WriteLine(emp);
            Console.WriteLine("Add this employee record to the database?");
            Console.Write("[Y]es, [E]dit record, [R]estart");

            char choice = GetUserInputChar();

            // user confirms the new employee record
            if (char.ToUpper(choice) == 'Y')
            {
                Console.WriteLine("Employee successfully added to database!");

                // add new employee record to the list
                payables.Add(emp);

                // prompt user to add another record
                Console.Write("Would you like to add another employee record (Y/N)? ");
                char addAgain = GetUserInputChar();

                if (char.ToUpper(addAgain) == 'Y')
                {
                    // clear console and call method to add new employee
                    Console.Clear();
                    AddNewEmployee();
                }
                else
                {
                    Console.WriteLine("Thank you for using our program. Have a nice day!");
                }
            }
            // user wishes to edit the record
            else if (char.ToUpper(choice) == 'E')
            {
                // temporarily store employee object
                // so it can be used for lookup
                payables.Add(emp);

                EditEmployee(emp);

                // confirm new changes with the user
                Console.WriteLine("Employee successfully added to database!");
            }
            // user wishes to start the process over
            else if (char.ToUpper(choice) == 'R')
            {
                // clear console and call method to add new employee
                Console.Clear();
                AddNewEmployee();
            }
            else
            {
                Console.WriteLine("Not a valid option. Terminating...");
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
                Console.WriteLine();
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
            Console.WriteLine($"\n\n\t\tEditing record of a: {employeeType}");
            EditEmployeeMenu();
            char choice = GetUserInputChar();
            bool validChoice = false;
            while (validChoice == false)
            {
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
                            validChoice = true;
                            break;
                        // these cases notify user that they chose an invalid option for
                        // a Salaried employee
                       // case 'W':
                       // case 'w':
                       // case 'H':
                       // case 'h':
                       // case 'G':
                       // case 'g':
                       // case 'C':
                       // case 'c':
                       // case 'B':
                       // case 'b':
                        default:
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
                            validChoice = true;
                            break;
                        case 'H':
                        case 'h':
                            Console.Write("\nENTER new hours worked: ");
                            string hours = Console.ReadLine();
                            hourlyEmployee.Wage = hourlyEmployee.ValidateDecimal(hours);
                            validChoice = true;
                            break;
                        // these cases notify user that they chose an invalid option for
                        // a Salaried employee
                        //case 'S':
                        //case 's':
                        //case 'G':
                        //case 'g':
                        //case 'C':
                        //case 'c':
                        //case 'B':
                        //case 'b':
                        default:
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
                            validChoice = true;
                            break;
                        case 'C':
                        case 'c':
                            Console.Write("\nENTER new commission rate: ");
                            string rate = Console.ReadLine();
                            commisionEmployee.CommissionRate = commisionEmployee.ValidateDecimal(rate);
                            validChoice = true;
                            break;
                        // these cases notify user that they chose an invalid option for
                        // a Salaried employee
                        default:
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
                            validChoice = true;
                            break;
                        // these cases notify user that they chose an invalid option for
                        // a Salaried employee
                        default:
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
                        validChoice = true;
                        break;
                    case 'L':
                    case 'l':
                        Console.Write("\nENTER new last name: ");
                        emp.LastName = Console.ReadLine();
                        validChoice = true;
                        break;
                    // gives user option to quit to main menu
                    case 'X':
                    case 'x':
                    case 'Q':
                    case 'q':
                        Console.Clear();
                        GoPayroll();
                        validChoice = true;
                        break;
                    /////////////// case '`':
                    //////////////     BackdoorAccess();
                    /////////////      break;
                    default:
                        Console.WriteLine($"Current record for: \n{emp}");
                        EditEmployee(emp);
                        break;
                }
            }
            Console.WriteLine($"\nEDIT operation done. \nCurrent record info:\n{emp}\nPress any key to continue.");
            Console.ReadKey();
            FromMain = false;
            EditEmployee(emp);
        }

        // function: Validates user input as a deicmal and reprompts them if it is not.
        // precondition: 
        // input: a singl
        // output: the va
        // postcondition:
        /*private decimal ValidateInteger(string number)
        {
            if (!(decimal.TryParse(number, out decimal result)))
            {
                Console.Write("Please ENTER decimal values ONLY: ");
                number = Console.ReadLine();
                result = ValidateInteger(number);
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



        // searches the list of payables for an employee
        // with a matching Email address. If found,
        // return the employee else null
        private Employee FindEmployeeRecord(out string email)
        {
            Console.Write("\nENTER the email address of the employee to search: ");
            email = Console.ReadLine();
            List<Employee> temp = new List<Employee>();
            foreach (IPayable payable in payables)
            {
                if (payable.GetType().Name != "Invoice")
                {
                    temp.Add((Employee) payable);
                }
            }
            foreach (Employee emp in temp)
            {
                if (email == emp.EmailAddress)
                {
                    Console.WriteLine($"FOUND email address: {emp.EmailAddress}");
                    Console.WriteLine($"Employee record:\n{emp}");
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
            decimal totalPay = 0;
            decimal totalInvoice = 0;
            Console.WriteLine();
            foreach (IPayable payable in payables)
            {

                // Attempting to format CommissionEmployee type name to 35 spaces like the rest of the classes
                // caused a large gap between "Base-salaried" and "Commission employ" when printing
                // This allows the CommissionEmployee type to be displayed as if it were formatted for 35 spaces.
                Console.WriteLine("\n*******************************************************");
                if (payable.GetType().Name == "CommissionEmployee")
                {
                    Console.Write("\t      ");
                    Console.WriteLine(payable);
                }
                else
                {
                    Console.WriteLine(payable);
                }

                Console.WriteLine($"{"Amount due: ",35}{payable.GetPaymentAmount():c}");
                switch (payable.GetType().Name)
                {
                    case "Invoice":
                        totalInvoice += payable.GetPaymentAmount();
                        break;
                    default:
                        totalPay += payable.GetPaymentAmount();
                        break;
                }
            }

            Console.WriteLine("\n*******************************************************");
            Console.WriteLine("\n\n\t******************************************");
            Console.WriteLine($"\t|   Payroll amount due: {totalPay,15:c}  | ");
            Console.WriteLine($"\t|   Invoice amount due: {totalInvoice,15:c}  | ");
            Console.WriteLine($"\t|     Total amount due: {totalInvoice + totalPay,15:c}  | ");
            Console.WriteLine("\t|                                        |");
            Console.WriteLine("\t******************************************");
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
            string payableType = string.Empty;
            while ((payableType = inFile.ReadLine()) != null)
            {
                if (payableType != "Invoice")
                {
                    // gather the essential information from the employee
                    string firstName = inFile.ReadLine();
                    string lastName = inFile.ReadLine();
                    string emailAdress = inFile.ReadLine();
                    string socialSecurityNumber = inFile.ReadLine();

                    if (payableType == "HourlyEmployee")
                    {
                        // get wage and hours worked
                        decimal hourlyWage = decimal.Parse(inFile.ReadLine());
                        decimal hoursWorked = decimal.Parse(inFile.ReadLine());

                        // create HourlyEmployee object and store into list
                        HourlyEmployee employee = new HourlyEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                            hourlyWage, hoursWorked);
                        payables.Add(employee);
                    }
                    else if (payableType == "SalariedEmployee")
                    {
                        // get weekly salary
                        decimal weeklySalary = decimal.Parse(inFile.ReadLine());

                        // create SalariedEmployee object and store into list
                        SalariedEmployee employee = new SalariedEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                            weeklySalary);
                        payables.Add(employee);
                    }
                    else if (payableType == "CommissionEmployee")
                    {
                        // get gross weekly sales and commission rate
                        decimal grossSales = decimal.Parse(inFile.ReadLine());
                        decimal commissionRate = decimal.Parse(inFile.ReadLine());

                        // create CommissionEmployee object and store into list
                        CommissionEmployee employee = new CommissionEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                            grossSales, commissionRate);
                        payables.Add(employee);
                    }

                    else if (payableType == "BasePlusCommissionEmployee")
                    {
                        // get gross weekly sales, commission rate, and base salary
                        decimal grossSales = decimal.Parse(inFile.ReadLine());
                        decimal commissionRate = decimal.Parse(inFile.ReadLine());
                        decimal baseSalary = decimal.Parse(inFile.ReadLine());

                        // create BasePlusCommissionEmployee object and store into list
                        BasePlusCommissionEmployee employee = new BasePlusCommissionEmployee(firstName, lastName, emailAdress, socialSecurityNumber,
                            grossSales, commissionRate, baseSalary);
                        payables.Add(employee);
                    }
                }
                else if (payableType == "Invoice")
                {
                    string invoiceNumber = inFile.ReadLine();
                    string partNumber = inFile.ReadLine();
                    string partDescription = inFile.ReadLine();
                    int quantity = int.Parse(inFile.ReadLine());
                    decimal pricePerItem = decimal.Parse(inFile.ReadLine());
                    Invoice invoice = new Invoice(invoiceNumber, partNumber, partDescription, quantity, pricePerItem);
                    payables.Add(invoice);
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
            foreach (/*Employee emp*/ IPayable item in payables)
            {
                // for now, echo the output to the shell for testing
                Console.WriteLine(item.ToStringForSaveFile());
                saveFile.WriteLine(item.ToStringForSaveFile());
            }

            // 3 - close the file object
            saveFile.Close();
            Environment.Exit(0);
        }

        public void TestMain()
        {
            // Read payables from input file
            //    ReadDataFromInputFile();
            // make 4 employee objects
            Employee emp1 = new SalariedEmployee("Albert:", "Albertson", "aalberston@job.com", "111111111", 3500.00m);
            Employee emp2 = new HourlyEmployee("Bonnie", "Bosch", "bbosch@job.com", "222222222", 15.45m, 40.00m);
            Employee emp3 = new CommissionEmployee("Charlie", "Cook", "ccook@job.com", "333333333", 7536.45m, 0.07m);
            Employee emp4 = new BasePlusCommissionEmployee("David", "Derelict", "dderelict@job.com", "444444444", 50097.05m, 0.05m, 3000m);


            // add the 4 objects to the list
            payables.Add(emp1);
            payables.Add(emp2);
            payables.Add(emp3);
            payables.Add(emp4);

            // make an anonymous object and put it in list
            payables.Add(new SalariedEmployee("Edgar", "Evengard", "eevengard@job.com", "555555555", 1300m));
            payables.Add(new HourlyEmployee("Fred", "Flinstone", "fflinstone@job.com", "666666666", 17.35m, 50m));
            payables.Add(new Invoice("000597", "01234", "seat", 2, 375.00M));
            payables.Add(new CommissionEmployee("Gina", "Gladstone", "ggladstone@job.com", "777777777", 7500m, 0.08m));
            payables.Add(new BasePlusCommissionEmployee("Howard", "Henderson", "hhenderson@job.com", "888888888", 7600m, .07m, 3400m));
            payables.Add(new Invoice("000703", "56789", "tire", 4, 79.95M));

            //Console.WriteLine(stu1);
            //Console.WriteLine(stu2);
            //Console.WriteLine(stu3);
        }
    }
}