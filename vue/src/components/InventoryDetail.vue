<template>
    <div>
        <h1>Current Inventory: {{ inventory.inventoryName }}</h1>
        <button v-on:click="editInventory">Edit</button>
        <button v-on:click="deleteInventory">Delete</button>
    </div>
</template>

<script>
import inventoriesServices from "../services/InventoriesServices";

export default {
    name: "inventory-detail",
    props: {
        id: String,
    },
    data() {
        return {
            inventory: {},
        };
    },
    methods: {
        editInventory() {
            this.$router.push({ name: "EditInventoryView",  params: {inventoryId: this.inventory.inventoryId}});
        },
        deleteInventory() {
            if (confirm("Are you sure you want to delete this inventory? This action cannot be undone.")) {
                inventoriesServices.deleteInventory(this.inventory.inventoryId)
                .then(response => {
                    if (response.status === 204){
                        this.$router.push({name: 'home'});
                    }
                })
                .catch((error) => {
                if (error.response.status === 404) {
                    alert("Error deleting this inventory. This inventory may have been deleted or you have entered an invalid inventory id");
                    this.$router.push({name: 'home'});
                    }
                });
            }
        }
    },
    created() {
        let numId = +this.id;
        inventoriesServices.getInventoryDetail(numId).then((response) => {
            this.inventory = response.data;
        });
    }
}
</script>

<style></style>