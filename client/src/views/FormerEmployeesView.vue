<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useEmployeeStore } from "@/stores/employees";
import EmployeeComponent from "../components/EmployeeComponent.vue";

const employees = useEmployeeStore();
var dialogOpen = ref(false);

onMounted(() => {
  employees.fetchFormerEmployees();
});

function openDialog(employeeId: number) {
  alert(employeeId);
  dialogOpen.value = !dialogOpen.value;
}
</script>

<template>
  <ul v-for="employee in employees.employees" :key="employee.id">
    <EmployeeComponent
      @nameClicked="openDialog(employee.id)"
      :employee="employee"
    />
  </ul>
</template>
