import { Position } from "@/models/position";

export interface EmployeePosition {
  id: number;
  startDate: string;
  endDate: string;
  position: Position;
}
