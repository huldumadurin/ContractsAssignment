using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractsAssignment
{
    class Program
    {
        static void Main(string[] args)
        {

            Account a = new Account(50);
            Account b = new Account(0);

            //This works
            b.Deposit(a.Withdraw(40));

            //This does not
            b.Deposit(a.Withdraw(20));

            Console.Out.WriteLine(b.Balance);
            Console.ReadKey();
        }
    }
}
