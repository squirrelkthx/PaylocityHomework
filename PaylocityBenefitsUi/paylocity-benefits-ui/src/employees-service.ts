import http from "./http-common";

class EmployeesDataService {
    getAll() {
      return http.get<any[]>("/v1/Employees");
    }
  
    get(id: string) {
      return http.get<any>(`v1/Employees/${id}`);
    }
  }
  
  export default new EmployeesDataService();