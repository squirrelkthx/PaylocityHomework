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
}
