using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{

    public enum TypeOfAccount
    {
        Checking,
        Savings,
        Loan,
        CD
    }
   /// <summary>
    /// This is about a bank account
    /// </summary>
    public class Account
    {
        private static int lastAccountNumber = 0;

        #region Properties
        [Key]
        public int AccountNumber { get; private set; }
        [Required]
        [StringLength(50,ErrorMessage ="Email Address is limited to 50 characters")]
        public string EmailAddress { get; set; }
        public decimal Balance { get; private set; }  // only this class can set the balance
        [Required]
        public TypeOfAccount AccountType { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Methods
        public decimal Deposit (decimal amount)
        {
            Balance += amount;
            return Balance;
        }

        #region Constructors
        public Account()
        {
            AccountNumber = ++lastAccountNumber;
            CreatedDate = DateTime.UtcNow;
        }
        #endregion

        public void Withdraw(decimal amount)
        {

        }
        #endregion
    }
}
