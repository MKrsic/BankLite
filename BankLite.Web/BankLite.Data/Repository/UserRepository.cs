using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BankLite.Data.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public List<User> GetList()
        {
            return this.DbContext.User
                .Include(u => u.Role)
                .Include(u => u.UserData)
                .OrderBy(u => u.ID)
                .ToList();
        }

        public override User Find(int id)
        {
            return this.DbContext.User
                .Include(u => u.Role)
                .Include(u => u.UserData)
                .Where(u => u.ID == id)
                .FirstOrDefault();
        }
    }
}
