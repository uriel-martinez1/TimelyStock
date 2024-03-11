<template>
    <div>
        <inventory-detail :id="$route.params.inventoryId" />
    </div>
</template>

<script>
import InventoryDetail from '../components/InventoryDetail.vue';

export default {
    components: {
        InventoryDetail
    },
};
</script>

<style>
</style>