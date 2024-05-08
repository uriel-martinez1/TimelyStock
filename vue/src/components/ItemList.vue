<template>
    <div>
        <ul>
            <li v-for="item in items" v-bind:key="item.itemId">
                <router-link v-bind:to="{name: 'ItemView', params: {inventoryId: this.$route.params.inventoryId, itemId: item.itemId}}">
                    {{ item.itemName }}
                </router-link>
                <!-- <router-link>
                    {{ item.availableQuantity }}
                </router-link> -->
                <button v-on:click="editItem(item.itemId)">Edit</button>
                <button v-on:click="deleteItem(item.itemId)">Delete</button>
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
        },
        deleteItem(itemId) {
            if (confirm("Are you sure you want to delete this item? This action cannot be undone.")) {
                // we add the inventoryService, we will need the inventory Id and the itemId
                let inventoryId = this.$route.params.inventoryId;
                console.log(itemId);
                inventoriesServices.deleteItemByInventoryId(inventoryId, itemId)
                .then ((response) => {
                    if (response.status === 204){
                        this.$store.commit(
                            'SET_NOTIFICATION',
                            {
                                message: 'Item was deleted from inventory.',
                                type: 'success'
                            }
                        );
                        this.retrieveItems();
                        this.pushToinventoryView();
                    }
                })
                .catch ((error) => {
                    if (error.response.status === 404) {
                        this.$store.commit(
                            'SET_NOTIFICATION',
                            {
                                message: 'Failed to delete the item from inventory. Please try again.',
                                type: 'error'
                            }
                        );
                        this.pushToinventoryView();   
                    }
                });
            }
        },
        pushToinventoryView() {
            let inventoryId = this.$route.params.inventoryId;
            this.$router.push({name: 'InventoryView', params: {inventoryId: this.inventoryId}});
        },
        retrieveItems() {
            let id = this.$route.params.inventoryId;
            inventoriesServices.getItemsByInventoryId(id)
            .then((response) => {
                this.items = response.data;
            })
              
        },
    },
}
</script>

<style>
ul > li {
    list-style-type: none;
}
</style>