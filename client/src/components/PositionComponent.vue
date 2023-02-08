<script setup lang="ts">
import { defineProps } from "vue";
import { usePositionStore } from "@/stores/positions";

defineProps({
  id: Number,
  name: String,
});
</script>

<template>
  ID: {{ id }} | Názov: {{ name }}
  <button @click="usePositionStore().removePosition(id!)">Zmazať</button>
</template>
