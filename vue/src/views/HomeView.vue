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
    <button>Add Inventory</button>
  </div>
</template>

<script>
import InventoryList from '../components/InventoriesList.vue';

export default {
  components: {
    InventoryList,
  }
}
</script>

<style>
</style>
