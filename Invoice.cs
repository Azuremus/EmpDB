// * (C) Copyright 1992-2017 by Deitel & Associates, Inc. and    
// * Pearson Education, Inc. All Rights Reserved.                
using System;
using System.Net.Mail;

public class Invoice : IPayable
{
    // Fig. 12.12: Invoice.cs
    // Invoice class implements IPayable.
    public string InvoiceNumber { get; }
    public string PartNumber { get; }
    public string PartDescription { get; }
    private int quantity;
    private decimal pricePerItem;

    // four-parameter constructor
    public Invoice(string invoiceNumber, string partNumber, string partDescription, int quantity,
       decimal pricePerItem)
    {
        InvoiceNumber = invoiceNumber;
        PartNumber = partNumber;
        PartDescription = partDescription;
        Quantity = quantity; // validate quantity 
        PricePerItem = pricePerItem; // validate price per item 
    }

    // property that gets and sets the quantity on the invoice
    public int Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            if (value < 0) // validation
            {
                Console.WriteLine($"Quantity of {PartDescription} must be >= 0");
                Console.Write($"ENTER valid quantity for {PartDescription}: ");
                string qty = Console.ReadLine();
                Quantity = ValidateInteger(qty);
            }
            else
            {
                quantity = value;
            }
        }
    }

    // property that gets and sets the price per item
    public decimal PricePerItem
    {
        get
        {
            return pricePerItem;
        }
        set
        {
            if (value < 0) // validation
            {
                Console.WriteLine($"Price per item for {PartDescription} must be >=0");
                Console.Write($"ENTER valid unit price for {PartDescription}: ");
                string price = Console.ReadLine();
                PricePerItem = ValidateDecimal(price);
            }
            else
            {
                pricePerItem = value;
            }
        }
    }

    // return string representation of Invoice object
    public override string ToString() =>
       $"{"Invoice: ",22}{InvoiceNumber}\n{"Part number: ",35}{PartNumber} ({PartDescription})\n" +
       $"{"Quantity: ",35}{Quantity}\n{"Price per item: ",35}{PricePerItem:C}";

    public string ToStringForSaveFile()
    {
        string str = GetType() + "\n";
        str += $"{InvoiceNumber}\n";
        str += $"{PartNumber}\n";
        str += $"{PartDescription}\n";
        str += $"{Quantity}\n";
        str += $"{PricePerItem}";
        return str;
    }

    // method required to carry out contract with interface IPayable
    public decimal GetPaymentAmount() => Quantity * PricePerItem;

    public int ValidateInteger(string number)
    {
        if (!(int.TryParse(number, out int result)))
        {
            Console.Write("Please ENTER integer values ONLY: ");
            number = Console.ReadLine();
            result = ValidateInteger(number);
        }
        return result;
    }
    public decimal ValidateDecimal(string number)
    {
        if (!(decimal.TryParse(number, out decimal result)))
        {
            Console.Write("Please ENTER decimal values ONLY: ");
            number = Console.ReadLine();
            result = ValidateDecimal(number);
        }
        return result;
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
