using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BankLite.Data.Repository
{
    public class UserDataRepository
    {
        protected BankLiteDbContext DbContext = new BankLiteDbContext();

        public List<UserData> GetList()
        {
            return this.DbContext.UserData
                .Include(u => u.User)
                .OrderBy(u => u.User_ID)
                .ToList();
        }

        public UserData Find(int id)
        {
            return this.DbContext.UserData
                .Include(u => u.User)
                .Where(u => u.User_ID == id)
                .FirstOrDefault();
        }

        public void Add(UserData model)
        {
            model.CreatedAt = DateTime.Now;
            this.DbContext.UserData.Add(model);
            this.DbContext.SaveChanges();
        }

        public void Update(UserData model)
        {
            model.UpdatedAt = DateTime.Now;
            this.DbContext.Entry(model).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var entity = this.DbContext.UserData.Find(id);
            this.DbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                this.DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
