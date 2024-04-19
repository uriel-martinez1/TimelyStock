<template>
    <div>
        <ul>
            <li v-for="item in items" v-bind:key="item.itemId">
                <router-link v-bind:to="{name: 'ItemView', params: {inventoryId: this.$route.params.inventoryId, itemId: item.itemId}}">
                    {{ item.itemName }}
                </router-link>
                <button v-on:click="editItem">Edit</button>
            </li>
        </ul>
    </div>
</template>

<script>
import inventoriesServices from '../services/InventoriesServices';

export default{
    data() {
        return {
            items: []
        }
    },
    created() {
        const id = this.$route.params.inventoryId
        inventoriesServices.getItemsByInventoryId(id)
        .then ((response) =>{
            this.items = response.data;
        });
    },
    methods: {
        editItem(itemId) {
            this.$router.push({name: "EditItemView", params: {inventoryId: this.$route.params.inventoryId, itemId: itemId}})
        }
    }
}
</script>

<style>
ul > li {
    list-style-type: none;
}
</style>