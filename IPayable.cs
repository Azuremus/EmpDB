// * (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
// * Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.11: IPayable.cs
// IPayable interface declaration.
using System;

public interface IPayable
{
   decimal GetPaymentAmount(); // calculate payment; no implementation

    string ToStringForSaveFile();
}

