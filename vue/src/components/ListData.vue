<template>
    <ul>
        <li v-for="inventory in inventories" v-bind:key="inventory.id">
            {{ inventory.inventoryName }}
            <div>
                <router-link>Edit</router-link>
                <router-link>Delete</router-link>
                <router-link>Add Items</router-link>
            </div>
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