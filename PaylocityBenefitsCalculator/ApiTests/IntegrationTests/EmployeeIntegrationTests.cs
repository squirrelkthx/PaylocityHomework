using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

// NOTE FOR FULL TEST SUITE: getting the following error: No connection could be made because the target machine actively refused it.
// most stack overflow articles talk about networking issues and IIS settings, none of these seem related, as the UI is communicating with the API just fine
// as I am unsure if this is a gotcha in the provided code or something with my local environment
// I am writing the tests in a way that would pass if the connection were not having these issues.
public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDto>
        {
            new()
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
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                ProfileUrl = "https://cdn.nba.com/headshots/nba/latest/1040x760/893.png",
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    //task: make test pass
    //resolution: implementing Get(int id) returns the correct employee
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30),
            ProfileUrl = "https://cdn.nba.com/headshots/nba/latest/1040x760/2544.png"
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }

    [Fact]
    //task: make test pass
    //resolution: when the requested id does not return an employee the api returns NotFound();
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenUpdatingAnEmployee_ShouldReturnUpdatedEmployee()
    {
        Employee employee = new Employee() { Id = 1, FirstName = "new-name" };
        HttpContent httpContent = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
        var response = await HttpClient.PatchAsync($"/api/v1/employees/", httpContent);
        GetEmployeeDto responseData = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "new-name",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30),
            ProfileUrl = "https://cdn.nba.com/headshots/nba/latest/1040x760/2544.png"
        };
        await response.ShouldReturn(HttpStatusCode.OK, responseData);
    }

    [Fact]
    public async Task WhenDeletingAnExistingEmployee_ShouldReturnTrue()
    {
        var response = await HttpClient.DeleteAsync($"/api/v1/employees/1");
       
        await response.ShouldReturn(HttpStatusCode.OK, true);
    }

    [Fact]
    public async Task WhenDeletingInvalidEmployee_ShouldReturnNotFound()
    {
        var response = await HttpClient.DeleteAsync($"/api/v1/employees/{int.MinValue}");
       
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

