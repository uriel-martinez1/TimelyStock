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
        InventoriesServices.getInventories().then((response) => {
            this.inventories = response.data;
        })
    },
    
    methods: {
        loadInventories() {
            InventoriesServices.getInventories(this.$store.state.user.userId).then((response) => {
                this.inventories = response.data;
            })
        }
    }
}
</script>

<style>
</style>