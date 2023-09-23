import { useEffect, useState } from "react";
import "./App.css";
import EmployeesDataSerive from "./employees-service";
import DependentsDataSerive from "./dependents-service";
import { Dependent, Employee } from "./models/employee";
import { calcPaycheck } from "./Utils/cost-calcs";

function App() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  useEffect(() => {
    let allEmployees = EmployeesDataSerive.getAll();
    allEmployees.then((response: any) => {
      let employees: Employee[] = response.data.data;
      employees.forEach(async (employee: Employee) => {
        await DependentsDataSerive.getForEmployee(`${employee.id}`)
          .then((depResponse) => {
            if (Array.isArray(depResponse.data.data)) {
              employee.dependents = depResponse.data.data;
            }
          })
          .then(() => setEmployees(employees));
      });
    });
  }, []);

  return (
    <div className="App">
      <header className="header">
        <h2>Paylocity Employee Basketball Team</h2>
      </header>
      <div className="employees">
        {employees &&
          employees.map((employee: Employee) => {
            let dob = new Date(employee.dateOfBirth);
            let timeDiff = Math.abs(Date.now() - dob.getTime());
            let age = Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25);
            return (
              <div className="employee" key={`employee-${employee.id}`}>
                <div className="employee-img-container">
                  <div
                    className="employee-img"
                    style={{ backgroundImage: `url('${employee.profileUrl}')` }}
                  />
                </div>
                <div className="employee-name">
                  {employee.firstName} {employee.lastName}
                </div>
                <div className="employee-data-row">
                  <span>
                    <span className="employee-data-label">AGE: </span>
                    {age}
                  </span>
                  <span>
                    <span className="employee-data-label">DOB: </span>
                    {`${dob.toLocaleDateString()}`}
                  </span>
                </div>
                <div className="employee-data-row">
                  <span className="employee-data-label">SALARY:</span>
                  <span>${employee.salary.toLocaleString()} </span>
                </div>
                <div className="employee-data-row">
                  <span className="employee-data-label">PAYCHECK:</span>
                  <span>
                    {calcPaycheck(
                      employee.salary,
                      employee.dependents.length,
                      age
                    ).toLocaleString("en-US", {
                      style: "currency",
                      currency: "USD",
                    })}
                  </span>
                </div>
                {employee.dependents && employee.dependents.length > 0 && (
                  <>
                    <br />
                    <span className="employee-data-label">DEPENDENTS:</span>
                    <br />
                    <div>
                      <ul className="dependent-list">
                        {employee.dependents.map((dependent: Dependent) => {
                          return (
                            <div key={`dependant-${dependent.id}`}>
                              {dependent.firstName} {dependent.lastName}
                            </div>
                          );
                        })}
                      </ul>
                    </div>
                  </>
                )}
              </div>
            );
          })}
      </div>
    </div>
  );
}

export default App;
