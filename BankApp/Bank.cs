using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public static class Bank
    {
        private static BankModel db = new BankModel();  // this opens connection to our db
        /// <summary>
        /// Bank creates an account for user
        /// </summary>
        /// <param name="emailAddress">Email address of the account</param>
        /// <param name="accountType">Type of Account</param>
        /// <param name="initialDeposit">Initial amount to deposit</param>
        /// <returns>Returns the new account</returns>
        public static Account CreateAccount(string emailAddress, TypeOfAccount accountType = TypeOfAccount.Checking, decimal initialDeposit = 0)
        {
            // create and initialize object
            var account = new Account
            {
                EmailAddress = emailAddress,
                AccountType = accountType
                // balance is read-only, so cannot set deposit here
            };

            if (initialDeposit > 0)
            {
                account.Deposit(initialDeposit);
            }

            // add this account to list of Bank's accounts
            db.Accounts.Add(account);
            db.SaveChanges();

            return account;
        }

        public static List<Account> GetAllAccounts(string emailAddress)
        {
            // we need to change this next time
            // as this returns a reference to the data which could end up being changed
            return db.Accounts.Where(a => a.EmailAddress == emailAddress).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
        public static void Deposit(int accountNumber, decimal amount)
        {
            var account = db.Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();
            if (account == null)
                return;
            account.Deposit(amount);

            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                TypeOfTransaction = TransactionType.Credit,
                Description = "Branch Deposit",
                Amount = amount,
                AccountNumber = account.AccountNumber
            };

            // save to db
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }


        public static void Withdraw(int accountNumber, decimal amount)
        {
            try
            {
                Account account = GetAccountByAccountNumber(accountNumber);
                account.Deposit(amount);

                var transaction = new Transaction
                {
                    TransactionDate = DateTime.UtcNow,
                    TypeOfTransaction = TransactionType.Debit,
                    Description = "Branch Withdrawl",
                    Amount = amount,
                    AccountNumber = account.AccountNumber
                };

                // save to db
                db.Transactions.Add(transaction);
                db.SaveChanges();
            }
            catch
            {
                // normally would log exception here 
                // and re-throw up to next level
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        private static Account GetAccountByAccountNumber(int accountNumber)
        {
            var account = db.Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();
            if (account == null)
               throw new ArgumentOutOfRangeException($"Invalid Account Number Provided: {accountNumber}");
            return account;
        }

        public static List<Transaction> GetAllTransactions(int accountNumber)
        {
            return db.Transactions.Where(t => t.AccountNumber == accountNumber).OrderByDescending(t => t.TransactionDate).ToList();
        }


    }
}
