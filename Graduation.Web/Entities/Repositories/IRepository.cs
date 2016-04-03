using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Graduation.Web.Entities.Repositories
{
    public interface IRepository : IDisposable
    {
        DbSet<T> Get<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Commit();
    }
}

