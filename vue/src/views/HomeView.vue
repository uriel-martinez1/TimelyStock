<template>
  <div class="home">
    <h1>Welcome to TimelyStock</h1>
    <p>You must be authenticated to see this</p>
  </div>

  <div class="list">
    <h2>List of Inventories</h2>
    <InventoryList/>
  </div>

  <div class="add">
    <h2>Add new Inventory</h2>
    <button @click="goToAddInventoryView">Add Inventory</button>
  </div>

  <div v-if="notification" class="notification" :class="notification.type">
      {{ notification.message }}
  </div>
</template>

<script>
import InventoryList from '../components/InventoriesList.vue';

export default {
  components: {
    InventoryList,
  },
  methods: {
    goToAddInventoryView() {
      this.$router.push({name: 'AddInventoryView'});
    },
  },
  computed: {
    notification() {
        return this.$store.state.notification;
    }
  }
};
</script>

<style>
.notification {
  position: fixed;
  top: 20px;
  right: 20px;
  padding: 10px 20px;
  border-radius: 5px;
  color: #fff;
  z-index: 9999;
}

.success {
  background-color: green;
}

.error {
  background-color: red;
}
</style>
