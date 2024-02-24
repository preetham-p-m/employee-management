using Application.DTO;
using Domain.Employee;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistance;

namespace Application;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext dataContext;

    private readonly IValidator<EmployeeDto> validator;

    private readonly ILogger<EmployeeService> logger;

    public EmployeeService(ILogger<EmployeeService> logger, DataContext dataContext, IValidator<EmployeeDto> validator)
    {
        this.logger = logger;
        this.dataContext = dataContext;
        this.validator = validator;
    }

    public async Task<object> CreateEmployee(EmployeeDto employeeDto)
    {
        var validationResult = await this.validator.ValidateAsync(employeeDto);

        if (!validationResult.IsValid)
        {
            this.logger.LogError(validationResult.Errors.ToArray().ToString());
            return new ValidationResult(validationResult.Errors);
        }

        var employee = new Employee()
        {
            Name = employeeDto.Name,
            DateOfBirth = employeeDto.DateOfBirth,
            Status = EmployeeStatus.Active
        };

        var result = this.dataContext.Employees.Add(employee);
        await this.dataContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<IList<Employee>> GetEmployees()
    {
        return await this.dataContext.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeById(Guid id)
    {
        return await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<object> EditEmployee(Guid id, EmployeeDto employeeDto)
    {
        var employee = await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            throw new BadHttpRequestException("Employee not found");
        }

        var validationResult = await this.validator.ValidateAsync(employeeDto);

        if (!validationResult.IsValid)
        {
            this.logger.LogError(validationResult.Errors.ToArray().ToString());
            return new ValidationResult(validationResult.Errors);
        }

        employee.DateOfBirth = employeeDto.DateOfBirth;

        await this.dataContext.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> ChangeStatus(Guid id)
    {
        var employee = await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            throw new BadHttpRequestException("Employee not found");
        }

        employee.Status =
            employee.Status == EmployeeStatus.Active
                ? EmployeeStatus.Inactive
                : EmployeeStatus.Active;

        await this.dataContext.SaveChangesAsync();

        return employee;
    }

    public async Task<Boolean> DeleteEmployee(Guid id)
    {
        var employee = await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            throw new BadHttpRequestException("Employee not found");
        }

        this.dataContext.Remove(employee);

        return await this.dataContext.SaveChangesAsync() > 0;
    }
}
