<template>
    <div>
        <h1>Edit Item</h1>
        <div class="loading" v-if="isLoading">
            Loading...
        </div>
        <item-form v-else v-bind:item="item" />
    </div>
</template>

<script>
import itemForm from '../components/ItemForm.vue';
import InventoriesServices from '../services/InventoriesServices';

export default {
    components: {
        itemForm,
    }, 
    data() {
        return {
            item: {
                itemId: 0,
                itemName: "",
                productUrl: "",
                skuItemNumber: "",
                price: 0,
                availableQuantity: 0,
                reorderPoint: 0,
                reorderQuantity: 0,
                categoryId: 0,
                supplierId: 0
            },
            isLoading: true,
        };
    },
    created() {
        // we need to grab the inventoryId from the urlPath
        let inventoryIdFromPath = parseInt(this.$route.params.inventoryId);
        console.log(inventoryIdFromPath);
        // we need to grag the itemId from the urlPath
        let itemIdFromPath = parseInt(this.$route.params.itemId);
        console.log(itemIdFromPath);
        // we then make sure both values are derived from the path and valid ids
        if (inventoryIdFromPath != 0 && itemIdFromPath != 0) {
            this.getItem(inventoryIdFromPath, itemIdFromPath);
        }
    },
    methods: {
        getItem(inventoryId, itemId){
            InventoriesServices.getItemByInventoryIdAndItemId(inventoryId, itemId)
            .then((response) =>{
                this.item = response.data;
                // this is where we turn off the loading screen
                this.isLoading = false;
            })
            .catch((error) =>{
                if (error.response.status === 404) {
                    alert("Error getting the item. This item may have been deleted or you have entered an invalid item id");
                    this.$router.push({name: 'InventoryView', params: {inventoryId: this.$route.params.inventoryId}});
                }
            });
        }
    },
}
</script>

<style>
</style>