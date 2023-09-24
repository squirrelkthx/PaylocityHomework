using Api.Dtos.Dependent;
using Api.Models;

public class MockDependentProvider : IDependentProvider
{
    private readonly List<GetDependentDto> dependents = new List<GetDependentDto> {
            new()
            {
                Id = 1,
                EmployeeId = 2,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3)
            },
            new()
            {
                Id = 2,
                EmployeeId = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23)
            },
            new()
            {
                Id = 3,
                EmployeeId = 2,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18)
            },
            new()
            {
                Id = 4,
                EmployeeId = 3,
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
                LastName = "Jordan2", // added duplicate domestic partner for M.Jordan to test removing extra domesticpartners/spouses
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2)
            }
        };

    public GetDependentDto? GetDependent(int id)
    {
        return dependents.First(x => x.Id == id);
    }

    public List<GetDependentDto> GetDependentsForEmployee(int id)
    {
        List<GetDependentDto> matches = dependents.FindAll(x => x.EmployeeId == id);
        // rule: only 1 spouse or domestic partner is allowed, we will remove all after the first (this logic could be different as to which one we keep)
        var spousesAndPartners = matches.FindAll(x => x.Relationship == Relationship.DomesticPartner || x.Relationship == Relationship.Spouse);
        if (spousesAndPartners != null && spousesAndPartners.Count() > 1)
        {
            var invalidDependents = spousesAndPartners.GetRange(1, spousesAndPartners.Count() - 1);
            invalidDependents.ForEach(x => matches.Remove(x));
        }

        return matches;
    }

    public List<GetDependentDto> GetDependents()
    {
        return dependents;
    }

    public bool AddDependentToEmployee(int employeeId, Dependent dependent)
    {
        if (dependent.Relationship == Relationship.Spouse || dependent.Relationship == Relationship.DomesticPartner)
        {
            bool employeeAlreadyHasSpouseOrDomesticPartner = dependents.Any(x => x.EmployeeId == dependent.EmployeeId && (x.Relationship == Relationship.Spouse || x.Relationship == Relationship.DomesticPartner));
            if (employeeAlreadyHasSpouseOrDomesticPartner)
            {
                return false;
            }
        }

        List<int> listOfIds = dependents.Select(x => x.Id).ToList();
        listOfIds.Sort();
        int lastId = listOfIds.LastOrDefault();

        // these mappings could be moved to a constructor
        dependents.Add(new GetDependentDto()
        {
            Id = lastId + 1, // Id's could be refactored to GUID
            EmployeeId = dependent.EmployeeId,
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship
        });

        return true;
    }

    public bool RemoveDependentFromEmployee(int employeeId, int dependentId)
    {
        GetDependentDto? dependentToRemove = dependents.FirstOrDefault(x => x.Id == dependentId && x.EmployeeId == employeeId);
        if (dependentToRemove == null) {
            return false;
        }
        return dependents.Remove(dependentToRemove);
    }

    public GetDependentDto? UpdateDependent(Dependent dependent)
    {
        GetDependentDto? dependentToUpdate = dependents.FirstOrDefault(x => x.Id == dependent.Id);
        if (dependentToUpdate == null)
        {
            return null;
        }

        // these mappings could be moved to a constructor
        if (dependent.FirstName != null && dependent.FirstName != dependentToUpdate.FirstName)
        {
            dependentToUpdate.FirstName = dependent.FirstName;
        }
        if (dependent.LastName != null && dependent.LastName != dependentToUpdate.LastName)
        {
            dependentToUpdate.LastName = dependent.LastName;
        }
        if (dependent.DateOfBirth != dependentToUpdate.DateOfBirth)
        {
            dependentToUpdate.DateOfBirth = dependent.DateOfBirth;
        }

        return GetDependent(dependent.Id);
    }
}