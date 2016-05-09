using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graduation.Web.Entities;
using Graduation.Web.Entities.Repositories;
using Graduation.Web.Services;
using Ninject;
using Ninject.Web.Common;

namespace Graduation.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IRepository>().To<GenericRepository>().InRequestScope();
            kernel.Bind<ITriviaService>().To<TriviaService>().InRequestScope();
            kernel.Bind<DbContext>().To<TriviaContext>().InRequestScope();
        }
    }
}