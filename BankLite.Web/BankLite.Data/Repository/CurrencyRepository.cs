using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BankLite.Data.Repository
{
    public class CurrencyRepository : RepositoryBase<Currency>
    {
        public List<Currency> GetList()
        {
            return this.DbContext.Currency
                .OrderBy(c => c.ID)
                .ToList();
        }

        public override Currency Find(int id)
        {
            return this.DbContext.Currency
                .Where(c => c.ID == id)
                .FirstOrDefault();
        }
    }
}
