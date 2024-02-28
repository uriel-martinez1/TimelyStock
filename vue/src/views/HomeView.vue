<template>
  <div class="home">
    <h1>Welcome to TimelyStock</h1>
    <p>You must be authenticated to see this</p>
  </div>

  <div class="add">
    <h2>Add new Inventory</h2>
    <AddForm></AddForm>
  </div>

  <div class="list">
    <h2>List of Inventories</h2>
    <ListData></ListData>
  </div>
</template>

<script>
import ListData from '../components/ListData.vue';
import AddForm from '../components/AddForm.vue';

export default {
  components: {
    ListData,
    AddForm,
  },
  data() {
    return {

    }
  }
};
</script>

<style>

</style>
