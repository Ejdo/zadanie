import { defineStore } from "pinia";
import { Employee } from "@/models/employee";

export const useEmployeeStore = defineStore("employees", {
  state: () => {
    return { employees: [] as Employee[] };
  },
  actions: {
    async fetchEmployees() {
      fetch("http://localhost:5085/api/Employees")
        .then((response) => response.json())
        .then((data) => (this.employees = data));
    },
    async fetchFormerEmployees() {
      fetch("http://localhost:5085/api/Employees/former")
        .then((response) => response.json())
        .then((data) => (this.employees = data));
    },
    async addEmployee(employee: Employee) {
      employee.id = 0;
      fetch("http://localhost:5085/api/Employees/", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(employee),
      }).then(() => this.fetchEmployees());
    },
    async removeEmployee(employeeId: number) {
      fetch("http://localhost:5085/api/Employees/" + employeeId, {
        method: "DELETE",
      }).then(() => this.fetchEmployees());
    },
    async updateEmployee(employee: Employee) {
      fetch("http://localhost:5085/api/Employees/" + employee.id, {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(employee),
      }).then(() => this.fetchEmployees());
    },
    async archiveEmployee(employeeId: number, active: boolean) {
      fetch(
        "http://localhost:5085/api/Employees/" +
          employeeId +
          "?isActive=" +
          active,
        {
          method: "PATCH",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
        }
      ).then(() => this.fetchEmployees());
    },
  },
});
