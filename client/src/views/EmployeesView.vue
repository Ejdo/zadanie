<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useEmployeeStore } from "@/stores/employees";
import { usePositionStore } from "@/stores/positions";
import EmployeeComponent from "../components/EmployeeComponent.vue";
import { Employee } from "@/models/employee";

const employees = useEmployeeStore();
const positions = usePositionStore();
let detailsOpen = ref(false);
let editOpen = ref(false);
let employee = {} as Employee;

function openDetails(id: number) {
  detailsOpen.value = !detailsOpen.value;
  editOpen.value = false;
  // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
  employee = employees.employees.find((employee) => employee.id == id)!;
}

function openEdit(id: number) {
  editOpen.value = !editOpen.value;
  detailsOpen.value = false;
  // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
  employee = employees.employees.find((employee) => employee.id == id)!;
  positions.fetchPositions();
}

function createEmployee() {
  employee = {} as Employee;
  employee.id = -1;
  employee.joinDate = new Date().toISOString().substr(0, 10);
  employee.isActive = true;
  editOpen.value = !editOpen.value;
  detailsOpen.value = false;
  positions.fetchPositions();
}

onMounted(() => {
  employees.fetchEmployees();
});
</script>

<template>
  <button @click="createEmployee()">Pridať zamestnanca</button>
  <ul v-for="employee in employees.employees" :key="employee.id">
    <EmployeeComponent
      @nameClicked="openDetails(employee.id)"
      @editClicked="openEdit(employee.id)"
      :employee="employee"
    />
  </ul>
  <div class="employeeDetails" v-if="detailsOpen">
    <h2>Zamestnanec s id: {{ employee.id }}</h2>
    <p>Meno: {{ employee.firstName }}</p>
    <p>Priezvisko: {{ employee.lastName }}</p>
    <p>Adresa: {{ employee.address }}</p>
    <p>Dátum narodenia: {{ employee.birthDate }}</p>
    <p>Dátum nástupu: {{ employee.joinDate }}</p>
    <p>Plat: {{ employee.salary }}</p>
    <p>
      Aktuálna pozícia:
      {{
        employee.employeePositions[employee.employeePositions.length - 1]
          .position.name
      }}
    </p>
    Predošlé pozície:
    <ul
      v-for="employeePosition in employee.employeePositions"
      :key="employeePosition.id"
    >
      <li>
        {{
          employeePosition.position.name +
          " - " +
          "Dátum nástupu: " +
          employeePosition.startDate
        }}
        <span v-if="employeePosition.endDate != null">
          - Dátum skončenia: {{ employeePosition.endDate }}</span
        >
      </li>
    </ul>
  </div>
  <div class="employeeDetails" v-if="editOpen">
    <h2 v-if="employee.id >= 0">Upraviť zamestnanca s id: {{ employee.id }}</h2>
    <h2 v-else>Pridať zamestnanca</h2>
    Meno:
    <input v-model="employee.firstName" />
    <br />
    Priezvisko:
    <input v-model="employee.lastName" />
    <br />
    Adresa:
    <input v-model="employee.address" />
    <br />
    Dátum narodenia:
    <input type="date" v-model="employee.birthDate" />
    <br />
    Dátum nástupu:
    <input type="date" v-model="employee.joinDate" />
    <br />
    Plat:
    <input type="number" v-model="employee.salary" />
    <br />
    Pozícia:
    <select v-model="employee.positionId">
      <option
        v-for="position in positions.positions"
        :key="position.id"
        :value="position.id"
      >
        {{ position.name }}
      </option>
    </select>
    <br />
    <button v-if="employee.id >= 0" @click="employees.updateEmployee(employee)">
      Uložiť
    </button>
    <button
      v-else
      @click="
        () => {
          employees.addEmployee(employee);
          editOpen = false;
        }
      "
    >
      Pridať
    </button>
  </div>
</template>

<style>
.employeeDetails {
  border: 1px;
  border-style: solid;
  padding: 10px;
}
</style>
