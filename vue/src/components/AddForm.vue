<template>
    <form v-on:submit.prevent="submitForm" class="addForm">
        <label for="inventoryName">Inventory Name:</label>
        <br>
        <input type="text" id="inventoryName" name="inventoryName" v-model="newInventory.inventoryName"/>
        <br><br>
        <button v-on:click.prevent="saveNewInventory" :disabled="validate">Save</button>
    </form>
</template>

<script>
import InventoriesServices from '../services/InventoriesServices';

export default {
    data() {
        return {
            inventory: [],
            newInventory: {
                inventoryName: "",
            },
        };
    },
    computed: {
        validData() {
            return !this.newInventory.inventoryName;
        },
    },
    methods: {
        saveNewInventory() {
            if (!this.validateAddForm()) {
                return;
            }
            InventoriesServices
                .addInventory(this.newInventory)
                .then((response) => {
                    this.inventory = response.data;
                    this.resetForm();
                })
        },
        validateAddForm() {
            let msg = "";
            if(this.newInventory.inventoryName.length === 0) {
                msg += "The new inventory must have a name. ";
            }
            if (msg.length > 0) {
                this.$store.commit("SET_NOTIFICATION", msg);
                return false;
            }
            return true;
        },
        resetForm() {
            this.newInventory = {
                inventoryName: "",
            };
        },
    }
}
</script>

<style>
</style>