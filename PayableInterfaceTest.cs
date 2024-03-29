﻿// *(C)Copyright 1992 - 2017 by Deitel & Associates, Inc. and               *
// * Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.14: PayableInterfaceTest.cs
// Tests interface IPayable with disparate classes.
using System;
using System.Collections.Generic;
using EmpDB;
class PayableInterfaceTest
{
    public static void TestMain()
    {
        // create a List<IPayable> and initialize it with four 
        // objects of classes that implement interface IPayable
        var payableObjects = new List<IPayable>() {
         new Invoice("000235","01234", "seat", 2, 375.00M),
         new Invoice("000235","56789", "tire", 4, 79.95M),
         new SalariedEmployee("John", "Smith","test1@test.com", "111-11-1111", 800.00M),
         new SalariedEmployee("Lisa", "Barnes","test2@test.com", "888-88-8888", 1200.00M)};

        Console.WriteLine(
           "Invoices and Employees processed polymorphically:\n");

        // generically process each element in payableObjects
        foreach (var payable in payableObjects)
        {
            // output payable and its appropriate payment amount
            Console.WriteLine($"{payable}");
            Console.WriteLine(
               $"{"Payment due: ",35}{payable.GetPaymentAmount():C}\n");
        }
    }
}


/**************************************************************************

 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 **************************************************************************/
