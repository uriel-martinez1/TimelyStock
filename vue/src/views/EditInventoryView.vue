<template>
    <div>
        <h1>Edit Inventory</h1>
        <inventory-form v-bind:inventory="inventory"/>
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
            inventory: {}
        };
    },

    methods: {
        getInventory(inventoryId){
            inventoriesServices.getInventoryDetail(inventoryId)
            .then(response =>{
                this.inventory = response.data;
            })
            .catch((error) => {
                if (error.response.status === 404) {
                    alert("Error getting inventory. This inventory may have been deleted or you have entered an invalid inventory id");
                    this.$router.push({name: 'home'});
                }
            });
        }
    },
    created() {
        this.getInventory(this.$route.params.inventoryId);


        // let inventoryId = parseInt(this.$route.params.inventoryId);
        // if(inventoryId != 0) {
        //     inventoriesServices.getInventoryDetail(inventoryId).then((response) =>{
        //         console.log(response.data);
        //         this.inventoryObj = response.data;
        //     })
        //     .catch((error) => {
        //         if (error.response.status === 404) {
        //             alert("Error getting card. This card may have been deleted or you have entered an invalid card id");
        //             this.$router.push({name: 'home'});
        //         }
        //     });
        // }
    }
}

</script>

<style>
</style>