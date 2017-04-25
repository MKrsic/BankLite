using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankLite.Data;
using BankLite.Model;
using BankLite.Data.Repository;
using BankLite.Web.Extensions;

namespace BankLite.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class TransactionsAdminController : Controller
    {
        private TransactionRepository TransactionRepository = new TransactionRepository();
        private BankAccountRepository BankAccountRepository = new BankAccountRepository();
        private ExchangeRateRepository ExchangeRateRepository = new ExchangeRateRepository();

        // GET: Transactions
        public ActionResult Index()
        {
            var transaction = TransactionRepository.GetList();
            return View(transaction.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = TransactionRepository.Find(id.Value);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.ExchangeRate_ID = new SelectList(ExchangeRateRepository.GetList(), "ID", "ID");
            ViewBag.BankAccount_From = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID()), "IBAN", "IBAN");
            ViewBag.BankAccount_To = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID(), true), "IBAN", "IBAN");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankAccount_From,BankAccount_To,Amount")] Transaction transaction)
        {
            transaction.ExchangeRate_ID = ExchangeRateRepository.FindLast().ID;
            if (ModelState.IsValid)
            {
                BankAccount bankAccount = new BankAccount();
                BankAccount bankAccount_To = new BankAccount();
                bankAccount = BankAccountRepository.Find(transaction.BankAccount_From);
                bankAccount_To = BankAccountRepository.Find(transaction.BankAccount_To);
                if (bankAccount != null && bankAccount_To != null && bankAccount.MoneyAmount >= transaction.Amount)
                {
                    TransactionRepository.Add(transaction);
                    bankAccount.MoneyAmount = bankAccount.MoneyAmount - transaction.Amount;
                    BankAccountRepository.Update(bankAccount);
                    bankAccount_To.MoneyAmount = bankAccount_To.MoneyAmount + transaction.Amount;
                    BankAccountRepository.Update(bankAccount_To);
                    return RedirectToAction("Index");
                }
                ViewBag.BankAccount_From = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID()), "IBAN", "IBAN", transaction.BankAccount_From);
                ViewBag.BankAccount_To = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID(), true), "IBAN", "IBAN", transaction.BankAccount_To);
                ViewBag.ExchangeRate_ID = new SelectList(ExchangeRateRepository.GetList(), "ID", "ID", transaction.ExchangeRate_ID);
                return View(transaction);
            }
            ViewBag.BankAccount_From = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID()), "IBAN", "IBAN", transaction.BankAccount_From);
            ViewBag.BankAccount_To = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID(), true), "IBAN", "IBAN", transaction.BankAccount_To);
            ViewBag.ExchangeRate_ID = new SelectList(ExchangeRateRepository.GetList(), "ID", "ID", transaction.ExchangeRate_ID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = TransactionRepository.Find(id.Value);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccount_From = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID()), "IBAN", "IBAN");
            ViewBag.BankAccount_To = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID(), true), "IBAN", "IBAN");
            ViewBag.ExchangeRate_ID = new SelectList(ExchangeRateRepository.GetList(), "ID", "ID", transaction.ExchangeRate_ID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BankAccount_From,BankAccount_To,Amount")] Transaction transaction)
        {
            transaction.ExchangeRate_ID = 1;
            var model = TransactionRepository.Find(transaction.ID);
            bool isOk = TryUpdateModel(model);
            if (ModelState.IsValid && isOk)
            {
                TransactionRepository.Update(model);
                return RedirectToAction("Index");
            }
            ViewBag.BankAccount_From = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID()), "IBAN", "IBAN", transaction.BankAccount_From);
            ViewBag.BankAccount_To = new SelectList(BankAccountRepository.GetList(User.Identity.GetUser_ID(), true), "IBAN", "IBAN", transaction.BankAccount_To);
            ViewBag.ExchangeRate_ID = new SelectList(ExchangeRateRepository.GetList(), "ID", "ID", transaction.ExchangeRate_ID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = TransactionRepository.Find(id.Value);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool ok = TransactionRepository.Delete(id);
            if (ok)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete", id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TransactionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
