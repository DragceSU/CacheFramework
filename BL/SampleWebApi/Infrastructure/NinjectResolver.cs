// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectResolver.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SampleWebApi.Infrastructure
{
    using System.Web.Http.Dependencies;

    using Ninject;

    /// <summary>
    /// </summary>
    public class NinjectResolver : NinjectScope, IDependencyResolver
    {
        /// <summary>
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        /// </summary>
        /// <param name="kernel">
        /// </param>
        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            this._kernel = kernel;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public IDependencyScope BeginScope()
        {
            return new NinjectScope(this._kernel.BeginBlock());
        }
    }
}