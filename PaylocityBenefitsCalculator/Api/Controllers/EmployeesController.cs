using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeProvider _employeeProvider;
    public EmployeesController(IEmployeeProvider employeeProvider) {
        _employeeProvider = employeeProvider;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = _employeeProvider.GetEmployee(id);
        if (employee == null) {
            return NotFound(); // requirements stated: "return 404", one could alternatively return an ApiResponse with success = false and an error message of 404
        }

        var result = new ApiResponse<GetEmployeeDto>
        {
            Data = employee,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: implement in a more production way
        //resolution: moved the static collection of employees into a DI provider
        var employees = _employeeProvider.GetEmployees();

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Add new employee")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> AddEmployee(Employee employee)
    {
        var employees = _employeeProvider.GetEmployees();
        if (employees.Any(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName))
        {
            return Conflict(); // existing employee found, return 409
        }

        GetEmployeeDto addedEmployee = _employeeProvider.AddEmployee(employee);

        var result = new ApiResponse<GetEmployeeDto>
        {
            Data = addedEmployee,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Delete an employee")]
    [HttpDelete]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteEmployee(int id)
    {
        var employees = _employeeProvider.GetEmployees();
        if (!employees.Any(x => x.Id == id))
        {
            return NotFound(); // no existing employee found, return 404
        }

        bool employeeDeleted = _employeeProvider.DeleteEmployee(id);

        // some logic should be changed here, I did not have any specific ideas at the time, so I have left this as is (with data and succuss just being the same value)
        var result = new ApiResponse<bool>
        {
            Data = employeeDeleted,
            Success = employeeDeleted
        };

        return result;
    }

    [SwaggerOperation(Summary = "Update an employee")]
    [HttpPatch]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> UpdateEmployee(Employee employee)
    {
        var employees = _employeeProvider.GetEmployees();
        if (!employees.Any(x => x.Id == employee.Id))
        {
            return NotFound(); // no existing employee found, return 404
        }

        GetEmployeeDto? updatedEmployee = _employeeProvider.UpdateEmployee(employee);

        var result = new ApiResponse<GetEmployeeDto>
        {
            Data = updatedEmployee,
            Success = updatedEmployee != null ? true : false
        };

        return result;
    }
}
