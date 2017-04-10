using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BankLite.Data.Repository
{
    public class AccountTypeRepository : RepositoryBase<AccountType>
    {
        public List<AccountType> GetList()
        {
            return this.DbContext.AccountType
                .Include(a => a.BankAccounts)
                .OrderBy(a => a.ID)
                .ToList();
        }

        public override AccountType Find(int id)
        {
            return this.DbContext.AccountType
                .Include(a => a.BankAccounts)
                .Where(a => a.ID == id)
                .FirstOrDefault();
        }
    }
}
