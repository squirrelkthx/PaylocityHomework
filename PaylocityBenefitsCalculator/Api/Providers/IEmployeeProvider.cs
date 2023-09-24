using Api.Dtos.Employee;
using Api.Models;

public interface IEmployeeProvider {
    List<GetEmployeeDto> GetEmployees();
    GetEmployeeDto? GetEmployee(int id);
    GetEmployeeDto AddEmployee(Employee employee);
    bool DeleteEmployee(int id);
    GetEmployeeDto? UpdateEmployee(Employee employee);
}