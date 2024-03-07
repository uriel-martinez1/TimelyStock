<template>
    <div class="inventories">
        <ul>
            <li v-for="inventory in inventories" v-bind:key="inventory.inventoryId">
                <RouterLink v-bind:to="{name: 'InventoryView', params: {id: inventory.inventoryId}}">
                    {{ inventory.inventoryName }}
                </RouterLink>
            </li>
        </ul>
    </div>
</template>

<script>
import { RouterLink } from 'vue-router';
import InventoriesServices from '../services/InventoriesServices';

export default {
props: ['inventories'],
};
</script>

<style>
ul > li {
    list-style-type: none;
}
</style>