using Api.Dtos.Dependent;
using Api.Models;

public interface IDependentProvider
{
    List<GetDependentDto> GetDependents();
    GetDependentDto? GetDependent(int id);
    List<GetDependentDto> GetDependentsForEmployee(int id);
    bool AddDependentToEmployee(int employeeId, Dependent dependent);
    bool RemoveDependentFromEmployee(int employeeId, int dependentId);
    GetDependentDto? UpdateDependent(Dependent dependent);
}