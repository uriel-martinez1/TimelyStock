<template>
    <div>
        <inventory-detail :id="$route.params.inventoryId" />

        <item-list />

        <!--We need an add item button under the list-->
        <div>
            <h2>Add new Item</h2>
            <button v-on:click="addItem">Add Item</button>
        </div>
    </div>
</template>

<script>
import InventoryDetail from '../components/InventoryDetail.vue';
import ItemList from '../components/ItemList.vue';

export default {
    components: {
        InventoryDetail,
        ItemList
    },
    methods: {
        addItem() {
            this.$router.push({name: "AddItemView", params: {inventoryId: this.$route.params.inventoryId}});
        }
    }
};
</script>

<style>
</style>