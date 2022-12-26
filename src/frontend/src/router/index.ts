import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import HomeView from "@/views/HomeView.vue";
import SignupView from "@/views/SignupView.vue"
import SigninView from "@/views/SigninView.vue"
import ServerInfoView from "@/views/ServerInfoView.vue";
import ServerSuggest from "@/views/SuggestServerView.vue"
import ServerAdd from "@/views/AddServerView.vue"
import ServerUpdate from "@/views/UpdateServerView.vue"

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/signup",
    name: "signup",
    component: SignupView,
  },
  {
    path: "/signin",
    name: "signin",
    component: SigninView,
  },
  {
    path: "/server-info/:id",
    name: "server-info",
    component: ServerInfoView,
  },
  {
    path: "/server-suggest",
    name: "server-suggest",
    component: ServerSuggest,
  },
  {
    path: "/server-add",
    name: "server-add",
    component: ServerAdd,
  },
  {
    path: "/server-update/:id",
    name: "server-update",
    component: ServerUpdate,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
