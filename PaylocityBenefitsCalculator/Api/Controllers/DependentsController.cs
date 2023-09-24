using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentProvider _dependentProvider;
    public DependentsController(IDependentProvider dependentProvider)
    {
        _dependentProvider = dependentProvider;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = _dependentProvider.GetDependent(id);
        if (dependent == null) {
            return NotFound();
        }

        var result = new ApiResponse<GetDependentDto>
        {
            Data = dependent,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = _dependentProvider.GetDependents();

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all dependents for Employee")]
    [HttpGet("employee/{id}")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAllForEmployee(int id)
    {
        var dependents = _dependentProvider.GetDependentsForEmployee(id);

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Add dependent to employee")]
    [HttpPost()]
    public async Task<ActionResult<ApiResponse<bool>>> AddDependentToEmployee(int employeeId, Dependent dependent)
    {
        bool successfullyAddedDependent = _dependentProvider.AddDependentToEmployee(employeeId, dependent);

        var result = new ApiResponse<bool>
        {
            Data = successfullyAddedDependent,
            Success = successfullyAddedDependent
            // should add a message here when trying to add a duplicate spouse/domestic partner to indicate the issue or handle that usecase differently
        };

        return result;
    }

    [SwaggerOperation(Summary = "Remove dependent from Employee")]
    [HttpDelete()]
    public async Task<ActionResult<ApiResponse<bool>>> RemoveDependentToEmployee(int employeeId, int dependentId)
    {
        bool successfullyAddedDependent = _dependentProvider.RemoveDependentFromEmployee(employeeId, dependentId);

        var result = new ApiResponse<bool>
        {
            Data = successfullyAddedDependent,
            Success = successfullyAddedDependent
        };

        return result;
    }

    [SwaggerOperation(Summary = "Update dependent")]
    [HttpPut()]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> UpdateDependent(Dependent dependent)
    {
        GetDependentDto? updatedDependent = _dependentProvider.UpdateDependent(dependent);

        var result = new ApiResponse<GetDependentDto>
        {
            Data = updatedDependent,
            Success = updatedDependent != null ? true : false
            // if there is a failure a message could be added here with details
        };

        return result;
    }
}
