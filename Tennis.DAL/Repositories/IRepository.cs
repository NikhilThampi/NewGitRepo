using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.DAL.Repositories
{
    public interface  IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition);
        T GetSingle(Expression<Func<T, bool>> whereCondition);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate, string include);
        void Add(T entity);
        void Delete(T entity);
        void Delete(List<T> entities);
        void Update(T entity);
        void save();
        List<T> GetAllStoredProc(string spName, params object[] parameters);
        List<T> GetBySql(string sql);
    }
}
