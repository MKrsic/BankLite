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
    public class ExchangeRatesController : Controller
    {
        //private BankLiteDbContext db = new BankLiteDbContext();
        private ExchangeRateRepository ExchangeRateRepository = new ExchangeRateRepository();

        // GET: ExchangeRates
        public ActionResult Index()
        {
            return View(ExchangeRateRepository.GetList());
        }

        // GET: ExchangeRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExchangeRate exchangeRate = ExchangeRateRepository.Find(id.Value);
            if (exchangeRate == null)
            {
                return HttpNotFound();
            }
            return View(exchangeRate);
        }

        // GET: ExchangeRates/Create
        public ActionResult Create()
        {
            //ViewBag.Currency_ID = new SelectList(db.Currency, "ID", "Name");
            return View();
        }

        // POST: ExchangeRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Currency_ID,Value")] ExchangeRate exchangeRate)
        {
            if (ModelState.IsValid)
            {
                ExchangeRateRepository.Add(exchangeRate);
                return RedirectToAction("Index");
            }

            //ViewBag.Currency_ID = new SelectList(db.Currency, "ID", "Name", exchangeRate.Currency_ID);
            return View(exchangeRate);
        }

        // GET: ExchangeRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExchangeRate exchangeRate = ExchangeRateRepository.Find(id.Value);
            if (exchangeRate == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Currency_ID = new SelectList(db.Currency, "ID", "Name", exchangeRate.Currency_ID);
            return View(exchangeRate);
        }

        // POST: ExchangeRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Currency_ID,Value")] ExchangeRate exchangeRate)
        {
            var model = ExchangeRateRepository.Find(exchangeRate.ID);
            bool isOk = TryUpdateModel(model);
            if (ModelState.IsValid && isOk)
            {
                ExchangeRateRepository.Update(model);
                return RedirectToAction("Index");
            }
            return View(exchangeRate);
        }

        // GET: ExchangeRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExchangeRate exchangeRate = ExchangeRateRepository.Find(id.Value);
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
            ExchangeRate exchangeRate = ExchangeRateRepository.Find(id);
            bool ok = ExchangeRateRepository.Delete(id);
            if (ok)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete", id);
        }
    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            ExchangeRateRepository.Dispose();
            }
        base.Dispose(disposing);
        }
    }
}
