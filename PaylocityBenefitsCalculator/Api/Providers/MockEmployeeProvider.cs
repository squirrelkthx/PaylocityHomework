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

    public GetEmployeeDto? GetEmployee(int id)
    {
        return employees.First(x => x.Id == id);
    }

    public List<GetEmployeeDto> GetEmployees()
    {
        return employees;
    }
}