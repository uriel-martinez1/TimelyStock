import { createRouter as createRouter, createWebHistory } from 'vue-router'
import { useStore } from 'vuex'

// Import components
import HomeView from '../views/HomeView.vue';
import LoginView from '../views/LoginView.vue';
import LogoutView from '../views/LogoutView.vue';
import RegisterView from '../views/RegisterView.vue';
import InventoryView from '../views/InventoryView.vue';
import AddInventoryView from '../views/AddInventoryView.vue';
import EditInventoryView from '../views/EditInventoryView.vue';
import AddItemView from '../views/AddItemView.vue';
import ItemView from '../views/ItemView.vue';
import EditItemView from '../views/EditItemView.vue';
import SupplierView from '../views/SupplierView.vue';
import CategoryView from '../views/CategoryView.vue';

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */
const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/:inventoryId",
    name: "InventoryView",
    component: InventoryView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/create",
    name: "AddInventoryView",
    component: AddInventoryView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/:inventoryId/edit",
    name: "EditInventoryView",
    component: EditInventoryView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/:inventoryId/item/create",
    name: "AddItemView",
    component: AddItemView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/:inventoryId/item/:itemId/edit",
    name: "EditItemView",
    component: EditItemView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/:inventoryId/item/:itemId",
    name: "ItemView",
    component: ItemView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/:inventoryId/item/:itemId/supplier/:supplierId",
    name: "SupplierView",
    component: SupplierView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/inventories/:inventoryId/item/:itemId/category/:categoryId",
    name: "CategoryView",
    component: CategoryView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/login",
    name: "login",
    component: LoginView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/logout",
    name: "logout",
    component: LogoutView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/register",
    name: "register",
    component: RegisterView,
    meta: {
      requiresAuth: false
    }
  },
];

// Create the router
const router = createRouter({
  history: createWebHistory(),
  routes: routes
});

router.beforeEach((to) => {

  // Get the Vuex store
  const store = useStore();

  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    return {name: "login"};
  }
  // Otherwise, do nothing and they'll go to their next destination
});

export default router;
