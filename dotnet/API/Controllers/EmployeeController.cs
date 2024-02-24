using Application;
using Application.DTO;
using Domain.Employee;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpPost()]
    public async Task<object> CreateEmployee([FromBody] EmployeeDto employeeDto)
    {
        return await this.employeeService.CreateEmployee(employeeDto);
    }

    [HttpGet()]
    public async Task<IList<Employee>> GetEmployees()
    {
        return await this.employeeService.GetEmployees();
    }

    [HttpGet("{id}")]
    public async Task<Employee> GetEmployeeById([FromRoute] Guid id)
    {
        return await this.employeeService.GetEmployeeById(id);
    }

    [HttpPut("{id}")]
    public async Task<object> EditEmployee([FromRoute] Guid id, [FromBody] EmployeeDto employeeDto)
    {
        return await this.employeeService.EditEmployee(id, employeeDto);
    }

    [HttpPut("{id}/change-status")]
    public async Task<Employee> ChangeEmployeeStatus([FromRoute] Guid id)
    {
        return await this.employeeService.ChangeStatus(id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
    {
        var result = await this.employeeService.DeleteEmployee(id);

        return result ? Ok() : BadRequest("Unable to delete employee");
    }
}
