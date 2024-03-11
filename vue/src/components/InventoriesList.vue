<template>
    <div class="inventories">
        <ul>
            <li v-for="inventory in inventories" v-bind:key="inventory.inventoryId">
                <router-link v-bind:to="{name: 'InventoryView', params: {inventoryId: inventory.inventoryId}}">
                    {{ inventory.inventoryName }}
                </router-link>
            </li>
        </ul>
    </div>
</template>

<script>
import inventoriesServices from '../services/InventoriesServices.js';

export default {
    data() {
        return {
            inventories: []
        }
    },
    created() {
        inventoriesServices.getInventories()
        .then ((response) => {
            //console.log(response.data)
            this.inventories = response.data;
        });
    }
};
</script>

<style>
ul > li {
    list-style-type: none;
}
</style>