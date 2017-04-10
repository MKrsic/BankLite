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

namespace BankLite.Web.Controllers
{
    public class BankAccountsController : Controller
    {
        private BankLiteDbContext db = new BankLiteDbContext();

        // GET: BankAccounts
        public ActionResult Index()
        {
            var bankAccount = db.BankAccount.Include(b => b.AccountType).Include(b => b.User);
            return View(bankAccount.ToList());
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccount.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountType_ID = new SelectList(db.AccountType, "ID", "Type");
            ViewBag.User_ID = new SelectList(db.User, "ID", "UserName");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AccountType_ID,User_ID,CreatedAt,UpdatedAt")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.BankAccount.Add(bankAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountType_ID = new SelectList(db.AccountType, "ID", "Type", bankAccount.AccountType_ID);
            ViewBag.User_ID = new SelectList(db.User, "ID", "UserName", bankAccount.User_ID);
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccount.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountType_ID = new SelectList(db.AccountType, "ID", "Type", bankAccount.AccountType_ID);
            ViewBag.User_ID = new SelectList(db.User, "ID", "UserName", bankAccount.User_ID);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccountType_ID,User_ID,CreatedAt,UpdatedAt")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountType_ID = new SelectList(db.AccountType, "ID", "Type", bankAccount.AccountType_ID);
            ViewBag.User_ID = new SelectList(db.User, "ID", "UserName", bankAccount.User_ID);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccount.Find(id);
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
            BankAccount bankAccount = db.BankAccount.Find(id);
            db.BankAccount.Remove(bankAccount);
            db.SaveChanges();
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
