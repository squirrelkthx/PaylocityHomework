import http from "./http-common";

class DependentsDataService {
    getAll() {
      return http.get<any[]>("/v1/Dependents");
    }
  
    get(id: string) {
      return http.get<any>(`v1/Dependents/${id}`);
    }

    getForEmployee(id: string) {
        return http.get<any>(`v1/Dependents/employee/${id}`);
      }
  }
  
  export default new DependentsDataService();