using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Entity.DB;

namespace Tennis.DAL.Repositories
{
   public class UnitOfWork : IDisposable
    {
       //private TennisEntities _dbContext = new TennisEntities();
       private TennisEntities _dbContext = null;
       public UnitOfWork()
       {
           _dbContext = new TennisEntities();
       }
       /// <summary>
       /// Saves all pending changes
       /// </summary>
       /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
       public int Commit()
       {
           // Save changes with the default options

           int res = 0;
           try
           {
               res = _dbContext.SaveChanges();
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



           return res;
       }

       public Repository <T> GetRepInstance<T>() where T : class
       {
           return new Repository<T>(_dbContext);
       }
       /// <summary>
       /// Disposes the current object
       /// </summary>
       public void Dispose()
       {
           Dispose(true);
           GC.SuppressFinalize(this);
       }

       private bool disposed = false;
       /// <summary>
       /// Disposes all external resources.
       /// </summary>
       /// <param name="disposing">The dispose indicator.</param>
       protected virtual void Dispose(bool disposing)
       {
           if (!this.disposed)
           {
               if (disposing)
               {
                   _dbContext.Dispose();
               }
           }
           this.disposed = true;
       }
    }
}
