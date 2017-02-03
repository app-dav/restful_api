using Notes.Interfaces;
using Microsoft.Practices.Unity;

namespace Notes.Repository
{
    public class RepositoryFactory
    {
        static UnityContainer _container;
        static RepositoryFactory()
        {
            _container = new UnityContainer();
            _container.RegisterType<IRepository, Repository>();
        }

        public static IRepository GetRepository()
        {
            return _container.Resolve<IRepository>();
        }
    }
}
