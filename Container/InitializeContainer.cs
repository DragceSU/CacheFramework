using DAL.IRepository;
using DAL.Repository;
using Ninject;

namespace Container
{
    public class InitializeContainer
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}