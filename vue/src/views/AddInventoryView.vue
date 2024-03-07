<template>
    <div>
        <h1>Add new inventory</h1>
        <InventoryAddForm v-bind:inventory="inventory" />
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
                inventoryId: 0,
                userId: parseInt(this.$store.state.user.userId),
                inventoryName: '',
            },
        };
    },
};
</script>

<style>
</style>