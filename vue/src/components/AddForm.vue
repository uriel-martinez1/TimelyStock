<template>
    <form v-on:submit.prevent="submitForm" class="addForm">
        <label for="inventoryName">Inventory Name:</label>
        <br>
        <input type="text" id="inventoryName" name="inventoryName" v-model="newInventory.inventoryName"/>
        <br><br>
        <button v-on:click.prevent="saveNewInventory" :disabled="validData">Save</button>
    </form>
</template>

<script>
import InventoriesServices from '../services/InventoriesServices';

export default {
    data() {
        return {
            inventory: [],
            newInventory: {
                userId: null,
                inventoryName: "",
                
            },
        };
    },
    // this is for the lifecycle hook when the component is creating to grab the userId from the store
    created() {
            this.newInventory.userId = this.$store.state.user ? this.$store.state.user.userId : null;
        },
    // this is for validating that the input fields for a new inventory are filled
    computed: {
        validData() {
            return !this.newInventory.inventoryName || !this.newInventory.userId;
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
                    this.$emit('Inventory was added!', response.data);
                    this.inventory = response.data;
                    this.resetForm();
                    this.refreshHome();
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
                userId: this.$store.state.user ? this.$store.state.user.userId : null,
                inventoryName: "",
            };
        },
        refreshHome() {
            location.reload ? location.reload() : (location = this.$router.push({ name: "home" }));
        },

    }
}
</script>

<style>
</style>