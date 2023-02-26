///////////////////////////////////////////////////////
// TINFO 200 A, Winter 2023
// UWTacoma SET, Caleb Ghirmai and Ryan Enyeart-Youngblood
// 2023-03-04 - EmpDB - C# programming project - An employee payroll system
// A database for tracking payroll information for 4 different employee types.
// The program uses basic CRUD operations, with an additional Streamwriter
// function for storing information into a .txt file.

///////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ----- Description
// 2023-02-19 - Ryan Enyeart -- Initial creation of solution and pseudocode.
// 2023-02-19 - Ryan Enyeart -- Creation of Employee inheritance hierarchy data object classes.
// 2023-02-19 - Ryan Enyeart -- Creation of GoPayroll method.
// 2023-02-19 - Caleb Ghirmai - Creation of FindEmployeeRecord method.
// 2023-02-19 - Caleb Ghirmai - Creation of DisplayMainMenu method.
// 2023-02-21 - Ryan Enyeart -- Creation EditEmployeeRecord and DeleteEmployee methods.
// 2023-02-21 - Ryan Enyeart -- Creation of ValidateInteger method to validate user input.
// 2023-02-21 - Caleb Ghirmai - Creation of ReadDataFromInputFile method.
// 2023-02-21 - Caleb Ghirmai - Creation of PrintAllRecords method.
// 2023-02-23 - Ryan Enyeart -- Creation of Invoice class.
// 2023-02-23 - Ryan Enyeart -- Creation of IPayable interface.
// 2023-02-23 - Ryan Enyeart -- Creation of PayableInterfaceTest class.
// 2023-02-23 - Ryan Enyeart -- Creation of PayrollSystemTest class.
// 2023-02-23 - Caleb Ghirmai - Creation of AddNewEmployee method.
// 2023-02-25 - Ryan Enyeart -- Modified AddNewEmployee method to use the ValidateDecimal method.
// 2023-02-25 - Caleb Ghirmai - Creation of ConfirmAddEmployee method.
// 2023-02-25 - Caleb Ghirmai - Modified AddNewEmployee method to allow confirmation of new employee record.
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Preliminary testing of the data object class
            //TestMain();
            PayrollSystemTest main= new PayrollSystemTest();

            EmployeeDB payrollSystem = new EmployeeDB();

            payrollSystem.GoPayroll();
        }
    }
}
