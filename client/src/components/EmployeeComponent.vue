<script setup lang="ts">
import { defineProps, PropType, defineEmits, ref } from "vue";
import { Employee } from "@/models/employee";
import { useEmployeeStore } from "@/stores/employees";

var deleteClicked = ref(false);
const employees = useEmployeeStore();

const emit = defineEmits<{
  (e: "nameClicked"): void;
  (e: "editClicked"): void;
}>();
defineProps({
  employee: {
    type: Object as PropType<Employee>,
    required: true,
  },
});
</script>

<template>
  <span @click="emit('nameClicked')"
    >Meno:
    {{
      employee.firstName +
      " " +
      employee.lastName +
      " - " +
      employee.employeePositions[employee.employeePositions.length - 1].position
        .name
    }}</span
  >
  <button @click="emit('editClicked')">Upraviť</button>
  <button @click="deleteClicked = !deleteClicked">Vymazať</button>
  <button v-if="deleteClicked" @click="employees.removeEmployee(employee.id)">
    Vymazať úplne
  </button>
  <button
    v-if="deleteClicked"
    @click="employees.archiveEmployee(employee.id, false)"
  >
    Archivovať
  </button>
  <button
    v-if="!employee.isActive"
    @click="employees.archiveEmployee(employee.id, true)"
  >
    Vrátiť
  </button>
</template>

<style>
span {
  cursor: pointer;
}
button {
  margin-left: 10px;
}
</style>
