using Application.DTO;
using Application.Validation;
using FluentValidation;

namespace API.Extension
{
    public static class ValidationHelper
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<EmployeeDto>, EmployeeDtoValidator>();

            return services;
        }
    }
}
