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
        private static List<Account> accounts = new List<Account>();
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
    }
}
