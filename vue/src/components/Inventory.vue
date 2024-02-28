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
        loadInventories() {
            InventoriesServices.getInventories(this.$store.state.user.userId).then((response) => {
                //console.log("The functions in the methods object are being used.")
                this.inventories = response.data;
            })
        }
    }
}
</script>

<style>
</style>