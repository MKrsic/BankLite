using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLite.Data.Repository
{
    public abstract class RepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected BankLiteDbContext DbContext = new BankLiteDbContext();

        public virtual TEntity Find(int id)
        {
            return this.DbContext.Set<TEntity>()
                .Where(p => p.ID == id)
                .FirstOrDefault();
        }

        public void Add(TEntity model)
        {
            model.CreatedAt = DateTime.Now;
            this.DbContext.Set<TEntity>().Add(model);
            this.DbContext.SaveChanges();
        }

        public void Update(TEntity model)
        {
            model.UpdatedAt = DateTime.Now;
            this.DbContext.Entry(model).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }

        public virtual bool Delete(int id)
        {
            var entity = this.DbContext.Set<TEntity>().Find(id);
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
