<template>
    <div>
        <h1>Add new inventory</h1>
        <inventory-add-form v-bind:inventory="inventory" />
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
            inventory: {
                id: 0,
                userId: 0,
                name: '',
            },
        };
    }
};
</script>

<style>
</style>