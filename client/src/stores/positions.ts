import { defineStore } from "pinia";
import { Position } from "@/models/position";

export const usePositionStore = defineStore("positions", {
  state: () => {
    return { positions: [] as Position[] };
  },
  actions: {
    async fetchPositions() {
      fetch("http://localhost:5085/api/Positions")
        .then((response) => response.json())
        .then((data) => (this.positions = data));
    },
    async addPosition(positionName: string) {
      fetch("http://localhost:5085/api/Positions", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name: positionName }),
      }).then(() => this.fetchPositions());
    },
    async removePosition(positionId: number) {
      fetch("http://localhost:5085/api/Positions/" + positionId, {
        method: "DELETE",
      }).then(() => this.fetchPositions());
    },
  },
});
