import { Employee } from "../types/employee";
import { axiosService } from "./axios";

export const EmployeeService = {
  getEmployees: (): Promise<Employee[]> => axiosService.get("employee")
};

