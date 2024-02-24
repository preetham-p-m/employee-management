using Application.DTO;
using Domain.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Application;

public interface IEmployeeService
{
    Task<object> CreateEmployee(EmployeeDto employeeDto);

    Task<IList<Employee>> GetEmployees();

    Task<Employee> GetEmployeeById(Guid id);

    Task<object> EditEmployee(Guid id, EmployeeDto employeeDto);

    Task<Employee> ChangeStatus(Guid id);

    Task<Boolean> DeleteEmployee(Guid id);
}
