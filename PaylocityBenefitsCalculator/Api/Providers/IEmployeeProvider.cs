using Api.Dtos.Employee;

public interface IEmployeeProvider {
    List<GetEmployeeDto> GetEmployees();
    GetEmployeeDto? GetEmployee(int id);
}