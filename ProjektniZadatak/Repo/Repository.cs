using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektniZadatak.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ProjektniZadatak.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext db;

        public Repository(ProjektniZadatakContext context)
        {
            db = context;
        }
        public Repository()
        {

        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }
        
        public TEntity Get(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        //Korisceno ranije
        IEnumerable<TEntity> IRepository<TEntity>.FindById(int id)
        {
            yield return db.Set<TEntity>().Find(id);
        }

        public void Edit(TEntity tEntity)
        {
            db.Entry(tEntity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}