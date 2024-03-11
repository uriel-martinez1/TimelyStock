<template>
    <div>
        <h1>Add new inventory</h1>
        <InventoryAddForm v-bind:inventory="inventoryObj"/>
    </div>
</template>

<script>
import InventoryAddForm from '../components/InventoryAddForm.vue';

export default {
    components: {
        InventoryAddForm,
    },
    data() {
        return {
            inventoryObj: {},
        };
    }
};
</script>

<style>
</style>