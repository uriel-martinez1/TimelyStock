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