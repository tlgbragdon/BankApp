using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankApp;

namespace BankUI.Controllers
{
    public class AccountsController : Controller
    {
        private BankModel db = new BankModel();

        // GET: Accounts
        [Authorize]
        public ActionResult Index()
        {
            return View(Bank.GetAllAccounts(HttpContext.User.Identity.Name));
        }

        // GET: Accounts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Account account = Bank.GetAccountByAccountNumber(id.Value);
                return View(account);
            }
            catch (InvalidAccountException ax)
            {
                Session["ErrorMessage"] = ax.Message;
                throw;
            }
        }

        // GET: Accounts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "AccountNumber,EmailAddress,Balance,AccountType,CreatedDate")] Account account)
        {
            account.EmailAddress = HttpContext.User.Identity.Name;
            //if (ModelState.IsValid)
            //{
                Bank.CreateAccount(account.EmailAddress, account.AccountType);
                return RedirectToAction("Index");
            //}

           // return View(account);
        }

        // GET: Accounts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = Bank.GetAccountByAccountNumber(id.Value);  //id is a nullable type which isn't allowed in our factory method, so get Value
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "AccountNumber,EmailAddress,Balance,AccountType,CreatedDate")] Account account)
        {
            account.EmailAddress = HttpContext.User.Identity.Name;
 //           if (ModelState.IsValid)
 //           {
                Bank.EditAccount(account);
                return RedirectToAction("Index");
 //           }
//            return View(account);
        }
        //GET
        [Authorize]
        public ActionResult Deposit (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var account = Bank.GetAccountByAccountNumber(id.Value);
            return View(account);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Deposit(FormCollection controls)
        {
            var accountNumber = Convert.ToInt32(controls["AccountNumber"]);
            var amount = Convert.ToDecimal(controls["Amount"]);

            Bank.Deposit(accountNumber, amount);
            return RedirectToAction("Index");
        }


        //GET
        [Authorize]
        public ActionResult Withdraw(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var account = Bank.GetAccountByAccountNumber(id.Value);
            return View(account);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Withdraw(FormCollection controls)
        {
            var accountNumber = Convert.ToInt32(controls["AccountNumber"]);
            var amount = Convert.ToDecimal(controls["Amount"]);

            Bank.Withdraw(accountNumber, amount);
            return RedirectToAction("Index");
        }

        //GET
        [Authorize]
        public ActionResult Transactions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transactions = Bank.GetAllTransactions(id.Value);
            return View(transactions);

        }

        // GET: Accounts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = Bank.GetAccountByAccountNumber(id.Value);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int?  id)
        {
            try
            {
                Account account = Bank.GetAccountByAccountNumber(id.Value);
                Bank.DeleteAccount(account);
            }
            catch (InvalidAccountException ax)
                 
            {
                Session["ErrorMessage"] = ax.Message;
                throw;
            }
            catch (ApplicationException ax)
                 
            {
                Session["ErrorMessage"] = ax.Message;
                throw;
            }

            return RedirectToAction("Index");
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
