<template>
    <div>
        <inventory-detail :id="$route.params.inventoryId" />

        <item-list />
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
};
</script>

<style>
</style>