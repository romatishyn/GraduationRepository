using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Graduation.Web.Entities.Repositories
{
    public class GenericRepository : IRepository, IDisposable
    {
        private readonly TriviaContext _context;

        public GenericRepository(TriviaContext context)
        {
            _context = context;
        }

        public DbSet<T> Get<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //---------------------------------------------------
        //private GenericRepository db;

        //public TriviaRepository(TriviaContext context)
        //{
        //    this.db = context;
        //}

        //public IEnumerable<TriviaTest> GetAll()
        //{
        //    return db.TriviaTests;
        //}

        //public TriviaTest Get(int id)
        //{
        //    return db.TriviaTests.Find(id);
        //}

        //public void Create(TriviaTest test)
        //{
        //    db.TriviaTests.Add(test);
        //}

        //public void Update(TriviaTest test)
        //{
        //    db.Entry(test).State = EntityState.Modified;
        //}

        //public void Delete(int id)
        //{
        //    var test = db.TriviaTests.Find(id);
        //    if (test != null)
        //        db.TriviaTests.Remove(test);
        //}
    }
}
