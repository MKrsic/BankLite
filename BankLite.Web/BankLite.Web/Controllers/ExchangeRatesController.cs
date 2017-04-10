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
    public class ExchangeRatesController : Controller
    {
        private BankLiteDbContext db = new BankLiteDbContext();

        // GET: ExchangeRates
        public ActionResult Index()
        {
            var exchangeRate = db.ExchangeRate.Include(e => e.Currency);
            return View(exchangeRate.ToList());
        }

        // GET: ExchangeRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExchangeRate exchangeRate = db.ExchangeRate.Find(id);
            if (exchangeRate == null)
            {
                return HttpNotFound();
            }
            return View(exchangeRate);
        }

        // GET: ExchangeRates/Create
        public ActionResult Create()
        {
            ViewBag.Currency_ID = new SelectList(db.Currency, "ID", "Name");
            return View();
        }

        // POST: ExchangeRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Currency_ID,Value,CreatedAt,UpdatedAt")] ExchangeRate exchangeRate)
        {
            if (ModelState.IsValid)
            {
                db.ExchangeRate.Add(exchangeRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Currency_ID = new SelectList(db.Currency, "ID", "Name", exchangeRate.Currency_ID);
            return View(exchangeRate);
        }

        // GET: ExchangeRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExchangeRate exchangeRate = db.ExchangeRate.Find(id);
            if (exchangeRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.Currency_ID = new SelectList(db.Currency, "ID", "Name", exchangeRate.Currency_ID);
            return View(exchangeRate);
        }

        // POST: ExchangeRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Currency_ID,Value,CreatedAt,UpdatedAt")] ExchangeRate exchangeRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exchangeRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Currency_ID = new SelectList(db.Currency, "ID", "Name", exchangeRate.Currency_ID);
            return View(exchangeRate);
        }

        // GET: ExchangeRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExchangeRate exchangeRate = db.ExchangeRate.Find(id);
            if (exchangeRate == null)
            {
                return HttpNotFound();
            }
            return View(exchangeRate);
        }

        // POST: ExchangeRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExchangeRate exchangeRate = db.ExchangeRate.Find(id);
            db.ExchangeRate.Remove(exchangeRate);
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
