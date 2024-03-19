<template>
    <div>
        <ul>
            <li v-for="item in items" v-bind:key="item.itemId">
                {{ item.itemName }}
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
        console.log(id)
        inventoriesServices.getItemsByInventoryId(id)
        .then ((response) =>{
            this.items = response.data;
        });
    }
}
</script>

<style>
ul > li {
    list-style-type: none;
}
</style>