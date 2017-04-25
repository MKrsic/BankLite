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

namespace BankLite.Web.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {
        private BankAccountRepository BankAccountRepository = new BankAccountRepository();
        private UserRepository UserRepository = new UserRepository();
        private AccountTypeRepository AccountTypeRepository = new AccountTypeRepository();

        // GET: BankAccounts
        public ActionResult Index()
        {
            return View(BankAccountRepository.GetList(User.Identity.GetUser_ID()));
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = BankAccountRepository.Find(id.Value);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountType_ID = new SelectList(AccountTypeRepository.GetList(), "ID", "Type");
            ViewBag.User_ID = new SelectList(UserRepository.GetList(), "ID", "UserName");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountType_ID")] BankAccount bankAccount)
        {
            Random rnd = new Random();
            string iban = "HR";
            for (int i = 0; i < 19; i++) {
                int random = rnd.Next(0, 9);
                iban += random.ToString();
            }
            bankAccount.IBAN = iban;
            bankAccount.MoneyAmount = 0;
            bankAccount.User_ID = User.Identity.GetUser_ID();
            if (ModelState.IsValid)
            {
                BankAccountRepository.Add(bankAccount);
                return RedirectToAction("Index");
            }

            ViewBag.AccountType_ID = new SelectList(AccountTypeRepository.GetList(), "ID", "Type", bankAccount.AccountType_ID);
            ViewBag.User_ID = new SelectList(UserRepository.GetList(), "ID", "UserName", bankAccount.User_ID);
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = BankAccountRepository.Find(id.Value);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountType_ID = new SelectList(AccountTypeRepository.GetList(), "ID", "Type", bankAccount.AccountType_ID);
            ViewBag.User_ID = new SelectList(UserRepository.GetList(), "ID", "UserName", bankAccount.User_ID);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccountType_ID")] BankAccount bankAccount)
        {
            bool isOk = TryUpdateModel(bankAccount);
            if (ModelState.IsValid && isOk)
            {
                BankAccountRepository.Update(bankAccount);
                return RedirectToAction("Index");
            }
            ViewBag.AccountType_ID = new SelectList(AccountTypeRepository.GetList(), "ID", "Type", bankAccount.AccountType_ID);
            ViewBag.User_ID = new SelectList(UserRepository.GetList(), "ID", "UserName", bankAccount.User_ID);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = BankAccountRepository.Find(id.Value);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool ok = BankAccountRepository.Delete(id);
            if (ok)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete", id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BankAccountRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
