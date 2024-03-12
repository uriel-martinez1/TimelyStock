<template>
    <div>
        <h1>Current Inventory: {{ inventory.inventoryName }}</h1>
        <button v-on:click="editInventory">Edit</button>
        <button>Delete</button>
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
            if (confirm("Are you sure you want to delete this inventory all associated items? This action cannot be undone.")) {
                inventoriesServices.deleteInventory(this.inventory.inventoryId)
                .then(response => {
                    if (response.status === 200){
                        this.$router.push({name: 'home'});
                    }
                })
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