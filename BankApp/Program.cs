using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************");
            Console.WriteLine("Welcome to my bank!");
            Console.WriteLine("************************");

            while (true)
            {
                Console.WriteLine("Please choose an option below:");
                Console.WriteLine("0 to Exit");
                Console.WriteLine("1 to Create a New Account");
                Console.WriteLine("2 to Deposit to an Account");
                Console.WriteLine("3 to Withdraw");
                Console.WriteLine("4 to Print All Accounts");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        // prompt for user email address
                        Console.Write("Email Address: ");
                        var emailAddress = Console.ReadLine();

                        // prompt for type of account to create
                        Console.WriteLine("Account Type: ");
                        var accountTypes = Enum.GetNames(typeof(TypeOfAccount));
                        for (var i = 0; i < accountTypes.Length; i++)
                        {
                            Console.WriteLine($"    {i} for {accountTypes[i]}");
                        }
                        // convert string read from console to enum type
                        var accountType = (TypeOfAccount)Enum.Parse(typeof(TypeOfAccount), Console.ReadLine());

                        // prompt for deposit amount
                        Console.Write("Amount to deposit: ");
                        var amount = Convert.ToDecimal(Console.ReadLine());
                        var account = Bank.CreateAccount(emailAddress, accountType, amount);

                        Console.WriteLine($"Account Number: {account.AccountNumber}, Account Type: {account.AccountType}, Balance: {account.Balance}, Created Date: {account.CreatedDate}");
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        Console.Write("Email Address: ");
                        emailAddress = Console.ReadLine();
                        var accounts = Bank.GetAllAccounts(emailAddress);
                        foreach (var item in accounts)
                        {
                            Console.WriteLine($"Account Number: {item.AccountNumber}, Account Type: {item.AccountType}, Balance: {item.Balance}, Created Date: {item.CreatedDate}");

                        }
                        break;
                    default:
                        break;
                }

            }


        }
    }
    
}
