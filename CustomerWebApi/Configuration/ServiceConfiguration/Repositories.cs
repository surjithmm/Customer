using CustomerRepository.Repositories.Master;
using CustomerRepositoryContract.IRepositories.Master;

namespace CustomerWebApi.Configuration.ServiceConfiguration
{
    public static class Repositories
    {
        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }
            //Master
            serviceCollection.AddScoped<ICmrRepository,CmrRepository>();
           
        }
    }
}
