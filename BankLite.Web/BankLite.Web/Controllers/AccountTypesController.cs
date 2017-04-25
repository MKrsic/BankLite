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

namespace BankLite.Web.Controllers
{
    [Authorize]
    public class AccountTypesController : Controller
    {
        private AccountTypeRepository AccountTypeRepository = new AccountTypeRepository();

        // GET: AccountTypes
        public ActionResult Index()
        {
            return View(AccountTypeRepository.GetList());
        }

        // GET: AccountTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountType accountType = AccountTypeRepository.Find(id.Value);
            if (accountType == null)
            {
                return HttpNotFound();
            }
            return View(accountType);
        }

        // GET: AccountTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type")] AccountType accountType)
        {
            if (ModelState.IsValid)
            {
                AccountTypeRepository.Add(accountType);
                return RedirectToAction("Index");
            }

            return View(accountType);
        }

        // GET: AccountTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountType accountType = AccountTypeRepository.Find(id.Value);
            if (accountType == null)
            {
                return HttpNotFound();
            }
            return View(accountType);
        }

        // POST: AccountTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type")] AccountType accountType)
        {
            bool isOk = TryUpdateModel(accountType);
            if (ModelState.IsValid && isOk)
            {
                AccountTypeRepository.Update(accountType);
                return RedirectToAction("Index");
            }

            return View(accountType);
        }

        // GET: AccountTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountType accountType = AccountTypeRepository.Find(id.Value);
            if (accountType == null)
            {
                return HttpNotFound();
            }
            return View(accountType);
        }

        // POST: AccountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool ok = AccountTypeRepository.Delete(id);
            if (ok)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete", id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AccountTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
