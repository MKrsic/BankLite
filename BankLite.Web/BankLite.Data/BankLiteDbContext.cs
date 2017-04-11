using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BankLite.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankLite.Data
{
    public class BankLiteDbContext : IdentityDbContext<AppUser>
    {
        public BankLiteDbContext()
            : base("BankLiteDbContext", throwIfV1Schema: false)
        {
        }
        public static BankLiteDbContext Create()
        {
            return new BankLiteDbContext();
        }

        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<ExchangeRate> ExchangeRate { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserData> UserData { get; set; }
    }
}
