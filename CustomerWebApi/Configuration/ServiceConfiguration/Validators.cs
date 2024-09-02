using CustomerWebApi.DataTransferObject.Master.CmrDto;
using CustomerWebApi.DataTransferObject.Master.CustomerDto;
using CustomerWebApi.Validators;
using FluentValidation;

namespace CustomerWebApi.Configuration.ServiceConfiguration
{
    public static class Validators
    {
        public static void AddValidators(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));

            }
            serviceCollection.AddTransient<IValidator<CreateCmrRequestDto>,CreateCmrRequestDtoValidator>();
            serviceCollection.AddTransient<IValidator<UpdateCmrRequestDto>, UpdateCmrRequestDtoValidator>();
        }
    }
}
