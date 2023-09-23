using Api.Dtos.Dependent;

public interface IDependentProvider
{
    List<GetDependentDto> GetDependents();
    GetDependentDto? GetDependent(int id);
    List<GetDependentDto> GetDependentsForEmployee(int id);
}