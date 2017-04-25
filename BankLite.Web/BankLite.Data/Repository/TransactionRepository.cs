using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BankLite.Data.Repository
{
    public class TransactionRepository : RepositoryBase<Transaction>
    {
        public List<Transaction> GetList()
        {
            return this.DbContext.Transaction
                .Include(t => t.ExchangeRate)
                .OrderBy(t => t.ID)
                .ToList();
        }

        public List<Transaction> GetList(string IBAN)
        {
            return this.DbContext.Transaction
                .Include(t => t.ExchangeRate)
                .Where(t => t.BankAccount_From == IBAN)
                .OrderBy(t => t.ID)
                .ToList();
        }

        public override Transaction Find(int id)
        {
            return this.DbContext.Transaction
                .Include(t => t.ExchangeRate)
                .Where(t => t.ID == id)
                .FirstOrDefault();
        }
    }
}
