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

        #region Properties
        [Key]
        public int AccountNumber { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Email Address is limited to 50 characters")]
        public string EmailAddress { get; set; }
        public decimal Balance { get; set; }  // only this class can set the balance
        [Required]
        public TypeOfAccount AccountType { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        #endregion

        #region Constructors
        public Account()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }
        #endregion

        #region Methods
        public decimal Deposit (decimal amount)
        {
            Balance += amount;
            return Balance;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
                throw new ArgumentOutOfRangeException("amount", "Insufficient funds for withdrawl");

            Balance -= amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ApplicationException">ApplicationException if account to delete has a balance</exception>
        public void Delete()
        {
            if (Balance > 0)
                throw new ApplicationException("Cannot delete account that has a balance");

            IsActive = false;
        }

        #endregion
    }
}
