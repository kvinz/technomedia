using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Technomedia.Domain.Abstract;
using Technomedia.Domain.Concrete;

namespace Technomedia.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;  // Экземпляр ядра распознования зависимостей

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
            kernel.Bind<IUserRepository>().To<EFUserRepository>();
            kernel.Bind<ICaseRepository>().To<EFCaseRepository>();
            kernel.Bind<ITeamRepository>().To<EFTeamRepository>();
            kernel.Bind<ITeamUserRepository>().To<EFTeamUserRepository>();
        }
    }
}