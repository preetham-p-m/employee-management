using Application.DTO;
using Application.Validation;
using Domain.Employee;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext dataContext;

    public EmployeeService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<Employee> CreateEmployee(EmployeeDto employeeDto)
    {
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

    public async Task<Employee> EditEmployee(Guid id, EmployeeDto employeeDto)
    {
        var employee = await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            throw new BadHttpRequestException("Employee not found");
        }

        if (!string.IsNullOrWhiteSpace(employeeDto.Name))
        {
            employee.Name = employeeDto.Name;
        }

        employee.DateOfBirth = employeeDto.DateOfBirth;

        await this.dataContext.SaveChangesAsync();

        return employee;
    }

    public async Task<Boolean> DeleteEmployee(Guid id){
        var employee = await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            throw new BadHttpRequestException("Employee not found");
        }

        this.dataContext.Remove(employee);

        return await this.dataContext.SaveChangesAsync() > 0;

    }
}
