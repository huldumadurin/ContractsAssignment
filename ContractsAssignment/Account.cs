using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace ContractsAssignment
{
    class Account
    { 
        public int Balance { get; private set; }

        public Account(int initialBalance = 0)
        {
            Balance = initialBalance;
        }

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

        public void Deposit(int amount)
        {
            Contract.Requires<ArgumentOutOfRangeException>(amount > 0, "Amount must be positive.");

            Contract.EnsuresOnThrow<ArgumentOutOfRangeException>(Balance == Contract.OldValue<int>(Balance), "Amount must remain unchanged if an exception is thrown.");
            Contract.Ensures(Balance == (Contract.OldValue<int>(Balance) + amount));


            Balance = Balance + amount;
        }
    }
}
