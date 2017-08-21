using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProjektniZadatak.Models;
using System.Data.Entity;

namespace ProjektniZadatak.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> FindById(int Id);
        
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Edit(TEntity tEntity);
    }
}
