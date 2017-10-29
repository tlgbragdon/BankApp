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
                        PrintAllAccounts();
                        try
                        {
                            Console.Write("Account Number: ");
                            var acctNumber = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Amount to deposit: ");
                            amount = Convert.ToDecimal(Console.ReadLine());
                            Bank.Deposit(acctNumber, amount);

                            Console.WriteLine($"{amount} deposited to account {acctNumber}");
                        }
                        catch(FormatException)
                        {
                            Console.WriteLine("Either account number or amount is invalid");
                        }
                        catch(OverflowException)
                        {
                            Console.WriteLine("Either account number or amount is beyond the allowed range.");
                        }
                        catch(ArgumentOutOfRangeException ax)
                        {
                            Console.WriteLine(ax.Message);
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("Oops!  Something went wrong; please try again");
                        }
                        break;
                    case "3":
                        PrintAllAccounts();
                        Console.Write("Account Number: ");
                        var accountNumber = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Amount to withdraw: ");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        Bank.Withdraw(accountNumber, amount);
                        Console.WriteLine($"{amount} withdrawn from account {accountNumber}");
                        break;
                    case "4":
                        PrintAllAccounts();
                        break;
                    case "5":
                        PrintAllAccounts();
                        Console.Write("Account Number: ");
                        accountNumber = Convert.ToInt32(Console.ReadLine());
                        var transactions = Bank.GetAllTransactions(accountNumber);
                        foreach (var tran in transactions)
                        {
                            Console.WriteLine($"Id: {tran.TransactionId}, Date: {tran.TransactionDate}, Type: {tran.TypeOfTransaction}, Amount: {tran.Amount}, Description: {tran.Description}");
                        }
                        break;
                    default:
                        break;
                }

            }


        }

        private static void PrintAllAccounts()
        {
            Console.Write("Email Address: ");
            var emailAddress = Console.ReadLine();
            var accounts = Bank.GetAllAccounts(emailAddress);
            foreach (var item in accounts)
            {
                Console.WriteLine($"Account Number: {item.AccountNumber}, Account Type: {item.AccountType}, Balance: {item.Balance}, Created Date: {item.CreatedDate}");
            }
        }
    }
    
}
