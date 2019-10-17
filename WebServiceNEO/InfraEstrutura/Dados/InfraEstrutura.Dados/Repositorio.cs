using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public abstract class Repositorio<Tipo> where Tipo : class
    {

        public virtual List<Tipo> ListAll()
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Set<Tipo>().ToList();
            }
        }

        public virtual bool Update(Tipo item)
        {
            try
            {
                using (WSNEOEntities context = new WSNEOEntities())
                {
                    context.Entry<Tipo>(item).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool UpdateOrInsert(Tipo item)
        {
            try
            {
                using (WSNEOEntities context = new WSNEOEntities())
                {
                    context.Entry<Tipo>(item).State = System.Data.Entity.EntityState.Modified;
                    int rows = 0;
                    bool concurrency = false;
                    try
                    {
                        rows = context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        concurrency = true;
                        ex.Entries.Single().Reload();
                        rows = context.SaveChanges();
                    }
                    if (rows <= 0)
                    {
                        context.Entry<Tipo>(item).State = System.Data.Entity.EntityState.Added;
                        rows = context.SaveChanges();
                    }

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(Tipo item)
        {
            try
            {

                using (WSNEOEntities context = new WSNEOEntities())
                {
                    context.Entry<Tipo>(item).State = System.Data.Entity.EntityState.Added;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool DeleteItem(Tipo item)
        {
            try
            {
                using (WSNEOEntities context = new WSNEOEntities())
                {
                    context.Entry<Tipo>(item).State = System.Data.Entity.EntityState.Deleted;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
