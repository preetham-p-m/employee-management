using Application.DTO;
using Domain.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Application;

public interface IEmployeeService
{
    Task<Employee> CreateEmployee(EmployeeDto employeeDto);

    Task<IList<Employee>> GetEmployees();

    Task<Employee> GetEmployeeById(Guid id);

    Task<Employee> EditEmployee(Guid id, EmployeeDto employeeDto);

    Task<Boolean> DeleteEmployee(Guid id);
}
