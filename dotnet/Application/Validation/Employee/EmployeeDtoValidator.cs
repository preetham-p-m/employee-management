using Application.DTO;
using FluentValidation;

namespace Application.Validation
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(a => a.DateOfBirth).NotNull();
        }
    }
}
