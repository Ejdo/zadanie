<script setup lang="ts">
import { onMounted, ref } from "vue";
import { usePositionStore } from "@/stores/positions";
import PositionComponent from "../components/PositionComponent.vue";

const positions = usePositionStore();
let newPosition = ref(false);
let positionName = ref("");

onMounted(() => {
  positions.fetchPositions();
});
</script>

<template>
  <ul v-for="position in positions.positions" :key="position.id">
    <PositionComponent :id="position.id" :name="position.name" />
  </ul>
  <button @click="newPosition = !newPosition">Pridať pozíciu</button>
  <div v-if="newPosition">
    <h2>Nová pozícia</h2>
    <span>Názov pozície: </span>
    <input v-model="positionName" />
    <button @click="positions.addPosition(positionName)">Vytvoriť</button>
  </div>
</template>
