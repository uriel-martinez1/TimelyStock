<template>
  <div class="home">
    <h1>Welcome to TimelyStock</h1>
    <p>You must be authenticated to see this</p>
  </div>

  <div class="list">
    <h2>List of Inventories</h2>
    <InventoryList v-bind:inventories="inventories"/>
  </div>

  <!-- <div class="add">
    <h2>Add new Inventory</h2>
    <InventoryAddForm></InventoryAddForm>
  </div> -->
</template>

<script>
import InventoryList from '../components/InventoriesList.vue';
import InventoryAddForm from '../components/InventoryAddForm.vue';
import InventoriesServices from '../services/InventoriesServices';

export default {
  components: {
    InventoryList,
    //InventoryAddForm,
  },
  data() {
    return {
      inventories: [],
      showAddInventory: false,
      newInventory: {
        userId: null,
        inventoryName: "",
    },
  };
},
computed: {
  validData() {
    return !this.newInventory.inventoryName;
  }
},
  // this should grab all the inventories when a new inventory item was added
  methods: {
    getInventories() {
      InventoriesServices.getInventories(this.$store.state.user.userId)
      .then((response) => {
        this.inventories = response.data;
        console.log(this.$store.state.user.userId)
      })
    }
  },
  created() {
    this.getInventories()
  }
};
</script>

<style>

</style>../components/InventoriesList.vue
