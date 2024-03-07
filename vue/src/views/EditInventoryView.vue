<template>
    <div>
        <h1>Edit Inventory</h1>
        <InventoryAddForm v-bind:inventory="inventory" />
    </div>
</template>

<script>
import InventoryAddForm from '../components/InventoryAddForm.vue';
import InventoriesServices from '../services/InventoriesServices.js';

export default {
    components: {
        InventoryAddForm,
    },
    data() {
        return {
            inventory: {
                inventoryName: "",
            }
        }
    },
    created() {
        let inventoryId = parseInt(this.$route.params.inventoryId);
        if (inventoryId != 0) {
            InventoriesServices.getInventory(inventoryId)
            .then((response) =>{
                this.inventory = response.data;
            });
        }
    }
}

</script>

<style>
</style>