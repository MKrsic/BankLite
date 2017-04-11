using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BankLite.Data.Repository
{
    public class BankAccountRepository : RepositoryBase<BankAccount>
    {
        public List<BankAccount> GetList(int user_ID)
        {
            return this.DbContext.BankAccount
                .Include(b => b.AccountType)
                .Where(b => b.User_ID == user_ID)
                .OrderBy(b => b.ID)
                .ToList();
        }

        public List<BankAccount> GetList(bool admin)
        {
            return this.DbContext.BankAccount
                .Include(b => b.AccountType)
                .OrderBy(b => b.ID)
                .ToList();
        }

        public override BankAccount Find(int id)
        {
            return this.DbContext.BankAccount
                .Include(b => b.AccountType)
                .Where(b => b.ID == id)
                .FirstOrDefault();
        }
    }
}
