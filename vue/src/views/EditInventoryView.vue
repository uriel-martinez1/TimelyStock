<template>
    <div>
        <h1>Edit Inventory</h1>
        <inventory-form v-bind:inventory="inventoryObj"/>
    </div>
</template>

<script>
import InventoryForm from '../components/InventoryAddForm.vue';
import inventoriesServices from '../services/InventoriesServices'

export default {
    components: {
        InventoryForm
    },
    data() {
        return {
            inventoryObj: {}
        };
    },
    created() {
        // this is where we find the existing inventory item
        let inventoryId = parseInt(this.$route.params.inventoryId);
        //console.log(inventoryId);
        if(inventoryId != 0) {
            inventoriesServices.getInventoryDetail(inventoryId).then((response) =>{
                console.log(response.data);
                this.inventoryObj = response.data;
            })
            .catch((error) => {
                if (error.response.status === 404) {
                    alert("Error getting card. This card may have been deleted or you have entered an invalid card id");
                    this.$router.push({name: 'home'});
                }
            });
        }
    }
}

</script>

<style>
</style>