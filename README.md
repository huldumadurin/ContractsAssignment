# Code Contracts example

Shows how Design by Contract can be used to precisely define the behavior of C# code.

## Issues
The development team for the nuget package Code Contracts seems to have disappeared, and it has no support for newer editions of Visual Studio. I was unable to get the package to work, having tested with versions 17, 15, and 13. However, the code should still be sound.

## Contract

Here is a piece of code from the Account class:
```
public int Withdraw(int amount)
        {
            Contract.Requires<ArgumentOutOfRangeException>(amount > 0, "Amount must be positive.");
            Contract.Requires<ArgumentOutOfRangeException>(amount <= Balance, "Amount must not exceed Balance.");

            Contract.EnsuresOnThrow<ArgumentOutOfRangeException>(Balance == Contract.OldValue<int>(Balance), "Amount must remain unchanged if an exception is thrown.");
            Contract.Ensures(Balance == (Contract.OldValue<int>(Balance) - amount));
            Contract.Ensures(Contract.Result<int>() == amount);


            Balance = Balance - amount;
            return amount;
        }
```
I use two Contract.Requires statements to ensure that the amount being withdrawn is between 0 and whatever balance is in the account. When this is exceeded, an ArgumentOutOfRangeException is thrown (given that the compiled code has been postprocessed with 'ccrewrite'.

Contract.EnsuresOnThrow is a way of defining behavior when method execution is interrupted. In the case of an ArgumentOutOfRangeException being thrown, this ensures that the Account's balance is not changed (Using Contract.OldValue<T>(), a helper method to recall the state of an object before the current method was called. In this method's case, it's quite obvious that the logic that throws an ArgumentOutOfRangeException is ahead of any Balance manipulation, but in more complex code, this line is a good way of assuring users of your code that a failed call to the method will not result in any unexpected changes.

Contract.Ensures helps us define the state of our program after a method finishes successfully. Again, OldValue is used to refer back to the original value of Balance. The first one makes sure that Balance is changed by exactly the correct amount, and the second one states that the method returns that same amount.
