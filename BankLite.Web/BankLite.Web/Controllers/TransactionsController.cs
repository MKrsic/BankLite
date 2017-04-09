using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLite.Web.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: User/Transactions
        public ActionResult Index()
        {
            return View();
        }
    }
}