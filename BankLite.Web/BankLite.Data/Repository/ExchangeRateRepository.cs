using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BankLite.Data.Repository
{
    public class ExchangeRateRepository : RepositoryBase<ExchangeRate>
    {
        public List<ExchangeRate> GetList()
        {
            return this.DbContext.ExchangeRate
                .Include(e => e.Currency)
                .OrderBy(e => e.ID)
                .ToList();
        }

        public override ExchangeRate Find(int id)
        {
            return this.DbContext.ExchangeRate
                .Include(e => e.Currency)
                .Where(e => e.ID == id)
                .FirstOrDefault();
        }
    }
}
