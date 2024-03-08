<template>
    <div class="inventories">
        <ul>
            <li v-for="inventory in inventories" v-bind:key="inventory.inventoryId">
                <router-link v-bind:to="{name: 'InventoryView', params: {id: inventory.inventoryId}}">
                    {{ inventory.inventoryName }}
                </router-link>
            </li>
        </ul>
    </div>
</template>

<script>
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