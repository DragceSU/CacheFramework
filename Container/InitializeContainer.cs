// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializeContainer.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Container
{
    using DAL.IRepository;
    using DAL.Repository;

    using Ninject;

    /// <summary>
    /// </summary>
    public class InitializeContainer
    {
        /// <summary>
        /// </summary>
        /// <param name="kernel">
        /// </param>
        public static void Register(IKernel kernel)
        {
            kernel.Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
        }
    }
}