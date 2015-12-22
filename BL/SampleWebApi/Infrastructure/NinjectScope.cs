// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectScope.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SampleWebApi.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;

    using Ninject.Parameters;
    using Ninject.Syntax;

    /// <summary>
    /// </summary>
    public class NinjectScope : IDependencyScope
    {
        /// <summary>
        /// </summary>
        protected IResolutionRoot resolutionRoot;

        /// <summary>
        /// </summary>
        /// <param name="kernel">
        /// </param>
        public NinjectScope(IResolutionRoot kernel)
        {
            this.resolutionRoot = kernel;
        }

        /// <summary>
        /// </summary>
        /// <param name="serviceType">
        /// </param>
        /// <returns>
        /// </returns>
        public object GetService(Type serviceType)
        {
            var request = this.resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return this.resolutionRoot.Resolve(request).SingleOrDefault();
        }

        /// <summary>
        /// </summary>
        /// <param name="serviceType">
        /// </param>
        /// <returns>
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var request = this.resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return this.resolutionRoot.Resolve(request).ToList();
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            var disposable = (IDisposable)this.resolutionRoot;
            if (disposable != null)
            {
                disposable.Dispose();
            }

            this.resolutionRoot = null;
        }
    }
}