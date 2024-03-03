<template>
    <ul>
        <li v-for="inventory in inventories" v-bind:key="inventory.id">
            {{ inventory.inventoryName }}
        </li>
    </ul>
</template>

<script>
import InventoriesServices from '../services/InventoriesServices';

export default {
    data() {
        return {
            inventories: []
        }
    },

    created() {
        InventoriesServices.getInventories(this.$store.state.user.userId).then((response) => {
            //console.log("The created is the one used.")
            this.inventories = response.data;
        })
    },

    methods: {
        addNewInventory(newInventory) {
            this.inventories.push(newInventory);
        },
    }
}
</script>

<style>
</style>