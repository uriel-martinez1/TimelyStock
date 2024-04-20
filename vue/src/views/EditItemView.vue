<template>
    <div>
        <h1>Edit Item</h1>
        <item-form v-bind:item="item"></item-form>
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
            item: {}
        };
    },
    methods: {
        getItem(inventoryId, itemId){
            InventoriesServices.getItemByInventoryIdAndItemId(inventoryId, itemId)
            .then((response) =>{
                this.item = response.data;
            })
            .catch((error) =>{
                if (error.response.status === 404) {
                    alert("Error getting the item. This item may have been deleted or you have entered an invalid item id");
                    this.$router.push({name: 'InventoryView', params: {inventoryId: this.$route.params.inventoryId}});
                }
            });
        }
    }, 
    created() {
        // to make this work we somehow need to get the inventory id and the item id from the url path
        this.getItem(this.$route.params.inventoryId, this.$route.params.itemId);
    }
}
</script>

<style>
</style>