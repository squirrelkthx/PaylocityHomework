using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class DependentIntegrationTests : IntegrationTest
{
    [Fact]
    //task: make test pass
    //resolution: imeplemented the GetAll method on the dependents controller
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        var response = await HttpClient.GetAsync("/api/v1/dependents");
        var dependents = new List<GetDependentDto>
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
            },
            new()
            {
                Id = 4,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2)
            },
            new()
            {
                Id = 5,
                EmployeeId = 3,
                FirstName = "DP",
                LastName = "Jordan2",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2)
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, dependents);
    }

    [Fact]
    //task: make test pass
    //resolution: imeplemented the Get(int id) method on the dependents controller
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        var response = await HttpClient.GetAsync("/api/v1/dependents/1");
        var dependent = new GetDependentDto
        {
            Id = 1,
            FirstName = "Spouse",
            LastName = "Morant",
            Relationship = Relationship.Spouse,
            DateOfBirth = new DateTime(1998, 3, 3)
        };
        await response.ShouldReturn(HttpStatusCode.OK, dependent);
    }

    [Fact]
    //task: make test pass
    //resolution: return NotFound if the GetDependent returns null
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/dependents/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    //task: make test pass
    //resolution: return NotFound if the GetDependent returns null
    public async Task WhenAddingNewChildDependent_ShouldReturnOKWithTrue()
    {
        Dependent dependent = new Dependent() { EmployeeId = 1, FirstName = "new-dep", LastName = "end-dent", DateOfBirth = new DateTime(1976, 11, 12), Relationship = Relationship.Child };
        HttpContent httpContent = new StringContent(JsonSerializer.Serialize(dependent), Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync($"/api/v1/dependents/", httpContent);
        await response.ShouldReturn(HttpStatusCode.OK, true);
    }

    [Fact]
    //task: make test pass
    //resolution: return NotFound if the GetDependent returns null
    public async Task WhenAddingNewSpouseDependent_ShouldReturnOKWithFalse()
    {
        Dependent dependent = new Dependent() { EmployeeId = 3, FirstName = "new-dep", LastName = "end-dent", DateOfBirth = new DateTime(1976, 11, 12), Relationship = Relationship.Spouse };
        HttpContent httpContent = new StringContent(JsonSerializer.Serialize(dependent), Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync($"/api/v1/dependents/", httpContent);
        await response.ShouldReturn(HttpStatusCode.OK, false);
    }
}

