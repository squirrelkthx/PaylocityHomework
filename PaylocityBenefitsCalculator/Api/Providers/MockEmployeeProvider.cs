using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

public class MockEmployeeProvider : IEmployeeProvider
{

    private readonly List<GetEmployeeDto> employees = new List<GetEmployeeDto>
        {
            new ()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30),
                ProfileUrl = "https://cdn.nba.com/headshots/nba/latest/1040x760/2544.png"
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                ProfileUrl = "https://cdn.nba.com/headshots/nba/latest/1040x760/1629630.png",
                // Dependents = new List<GetDependentDto>
                // {
                //     new()
                //     {
                //         Id = 1,
                //         FirstName = "Spouse",
                //         LastName = "Morant",
                //         Relationship = Relationship.Spouse,
                //         DateOfBirth = new DateTime(1998, 3, 3)
                //     },
                //     new()
                //     {
                //         Id = 2,
                //         FirstName = "Child1",
                //         LastName = "Morant",
                //         Relationship = Relationship.Child,
                //         DateOfBirth = new DateTime(2020, 6, 23)
                //     },
                //     new()
                //     {
                //         Id = 3,
                //         FirstName = "Child2",
                //         LastName = "Morant",
                //         Relationship = Relationship.Child,
                //         DateOfBirth = new DateTime(2021, 5, 18)
                //     }
                // }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                ProfileUrl = "https://cdn.nba.com/headshots/nba/latest/1040x760/893.png",
                // Dependents = new List<GetDependentDto>
                // {
                //     new()
                //     {
                //         Id = 4,
                //         FirstName = "DP",
                //         LastName = "Jordan",
                //         Relationship = Relationship.DomesticPartner,
                //         DateOfBirth = new DateTime(1974, 1, 2)
                //     }
                // }
            }
        };

    public GetEmployeeDto AddEmployee(Employee employee)
    {
        List<int> listOfIds = employees.Select(x => x.Id).ToList();
        listOfIds.Sort();
        int lastId = listOfIds.LastOrDefault();

        // these mappings could be moved to a constructor
        GetEmployeeDto employeeToAdd = new GetEmployeeDto()
        {
            Id = lastId + 1, // Id's could be refactored to GUID
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfBirth = employee.DateOfBirth,
            ProfileUrl = employee.ProfileUrl
        };
        employees.Add(employeeToAdd);

        return employeeToAdd;
    }

    public bool DeleteEmployee(int id)
    {
        GetEmployeeDto? employeeToRemove = employees.Find(x => x.Id == id);
        if (employeeToRemove == null)
        {
            return false;
        }

        return employees.Remove(employeeToRemove);
    }

    public GetEmployeeDto? GetEmployee(int id)
    {
        return employees.First(x => x.Id == id);
    }

    public List<GetEmployeeDto> GetEmployees()
    {
        return employees;
    }

    public GetEmployeeDto? UpdateEmployee(Employee employee)
    {
        GetEmployeeDto? employeeToUpdate = GetEmployee(employee.Id);
        if (employeeToUpdate == null)
        {
            return null;
        }
        // these mappings could be moved to a constructor
        if (employee.FirstName != null && employee.FirstName != employeeToUpdate.FirstName)
        {
            employeeToUpdate.FirstName = employee.FirstName;
        }
        if (employee.LastName != null && employee.LastName != employeeToUpdate.LastName)
        {
            employeeToUpdate.LastName = employee.LastName;
        }
        if (employee.Salary != employeeToUpdate.Salary)
        {
            employeeToUpdate.Salary = employee.Salary;
        }
        if (employee.DateOfBirth != employeeToUpdate.DateOfBirth)
        {
            employeeToUpdate.DateOfBirth = employee.DateOfBirth;
        }
        if (employee.ProfileUrl != employeeToUpdate.ProfileUrl)
        {
            employeeToUpdate.ProfileUrl = employee.ProfileUrl;
        }

        return GetEmployee(employee.Id);
    }
}