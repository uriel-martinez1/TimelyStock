<template>
  <div class="home">
    <h1>Home</h1>
    <p>You must be authenticated to see this</p>
  </div>

  <div class="inventory">
    <h1>List of Inventories</h1>
    <Inventory></Inventory>
  </div>
</template>

<script>
import Inventory from '../components/Inventory.vue';

export default {
  components: {
    Inventory,
  },
  data() {
    return {

    }
  }
};
</script>

<style>

</style>
