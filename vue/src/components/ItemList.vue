<template>
    <div>
        <ul>
            <li v-for="item in items" v-bind:key="item.itemId">
                <router-link v-bind:to="{name: 'ItemView', params: {inventoryId: this.$route.params.inventoryId, itemId: item.itemId}}">
                    Item Name: {{ item.itemName }}
                </router-link>
                <br>
                <router-link v-bind:to="{name: 'SupplierView', params: {inventoryId: this.$route.params.inventoryId, itemId: item.itemId, supplierId: item.supplierId}}">
                    Supplier: {{ getSupplierName(item.supplierId) }}
                </router-link>
                <br>
                <button v-on:click="editItem(item.itemId)">Edit</button>
                <button v-on:click="deleteItem(item.itemId)">Delete</button>
            </li>
        </ul>
    </div>
</template>

<script>
import inventoriesServices from '../services/InventoriesServices';
import supplierServices from '../services/SuppliersServices';

export default{
    data() {
        return {
            items: [],
            supplier: {}, // object to store details
        }
    },
    created() {
        const id = this.$route.params.inventoryId
        inventoriesServices.getItemsByInventoryId(id)
        .then ((response) =>{
            this.items = response.data;
            // fetch supplier details when items are retrieved
            this.fetchSupplierDetail();
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
        // method to fetch supplier details based on supplierIdf
        fetchSupplierDetail() {
            this.items.forEach((item) => {
                supplierServices.getSupplierById(item.supplierId)
                    .then((response) => {
                        // store supplier details in the supplier object
                        console.log(response.data.supplierName);
                        this.supplier[item.supplierId] = response.data.supplierName;
                    });
            });
        },
        getSupplierName(supplierId) {
            return this.supplier[supplierId] || '';
        },
    },
}
</script>

<style>
ul > li {
    list-style-type: none;
}
</style>