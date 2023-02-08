import { EmployeePosition } from "./employeePosition";

export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  address?: string;
  birthDate: string;
  joinDate: string;
  salary: number;
  isActive: boolean;
  positionId: number;
  employeePositions: EmployeePosition[];
}
