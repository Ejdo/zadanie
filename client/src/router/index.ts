import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import EmployeesView from "../views/EmployeesView.vue";
import FormerEmployeesView from "../views/FormerEmployeesView.vue";
import PositionsView from "../views/PositionsView.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: EmployeesView,
  },
  {
    path: "/formerEmployees",
    name: "formerEmployees",
    component: FormerEmployeesView,
  },
  {
    path: "/positions",
    name: "positions",
    component: PositionsView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
