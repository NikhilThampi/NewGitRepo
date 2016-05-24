using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tennis.Entity.DB;

namespace Tennis.DAL.Repositories
{
    public class Repository<T> : IRepository <T> where T:class
    {
        private  TennisEntities _context;
        
        public Repository(TennisEntities context)
        {
            _context = context;
        }
        //public Repository()
        //{
        //    _context = TennisEntities();
        //}

        private TennisEntities TennisEntities()
        {
            throw new NotImplementedException();
        }


            
        
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().First(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return _context.Set<T>();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().Where(predicate);
            }
            catch
            {
                return null;
            }
        }
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate, string include)
        {
            try
            {
                return _context.Set<T>().Include(include).Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (_context.Entry(entity).State.Equals(EntityState.Detached))
                {
                    _context.Set<T>().Attach(entity);
                }

                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                //logService.Fatal("Error updating: " + ex.Message);
                throw new Exception("An error was encountered while updating. Please try again later.");
            }
        }

        public void Add(T entity)
           {
            try
            {
                _context.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                //logService.Fatal("Error inserting: " + ex.Message);
                throw new Exception("An error was encountered while inserting. Please try again later.");
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (_context.Entry(entity).State.Equals(EntityState.Detached))
                {
                    _context.Set<T>().Attach(entity);
                }
                _context.Set<T>().Remove(entity);

            }
            catch (Exception ex)
            {
                //logService.Fatal("Error deleting: " + ex.Message);
                throw new Exception("An error was encountered while deleting. Please try again later.");
            }
        }

        public void Delete(List<T> entities)
        {
            try
            {
                foreach (T entity in entities)
                {
                    if (_context.Entry(entity).State.Equals(EntityState.Detached))
                    {
                        _context.Set<T>().Attach(entity);
                    }

                    //_context.Entry(entity).State = EntityState.Deleted;
                    _context.Set<T>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                //logService.Fatal("Error deleting: " + ex.Message);
                throw new Exception("An error was encountered while deleting. Please try again later.");
            }
        }

        public void save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }




        }

        public List<T> GetAllStoredProc(string spName, params object[] parameters)
        {
            try
            {

                return _context.Database.SqlQuery<T>(spName, parameters).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error was encountered. Please try again later.");
            }
        }


        public List<T> GetBySql(string sql)
        {
            try
            {

                return _context.Database.SqlQuery<T>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error was encountered. Please try again later.");
            }
        }

    }
}
