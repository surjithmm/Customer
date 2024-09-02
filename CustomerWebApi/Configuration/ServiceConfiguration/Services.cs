using CustomerService.Service.Master;
using CustomerServiceContract.IService.Master;

namespace CustomerWebApi.Configuration.ServiceConfiguration
{
    public static class Services
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }
            //General
            serviceCollection.AddTransient<ICmrService,CmrService>();


        }
    }
}
