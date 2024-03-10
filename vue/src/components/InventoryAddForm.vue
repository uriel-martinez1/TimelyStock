<template>
    <form v-on:submit.prevent="submitForm" class="inventoryAddForm">
        <label for="inventoryName">Inventory Name:</label>
        <input id="inventoryName" type="text" class="form-control" v-model="editInventory.inventoryName" />
        <button v-on:click.prevent="saveNewInventory" :disabled="validData">Save</button>
        <button v-on:click="cancelForm">Cancel</button>
    </form>
</template>

<script>
import InventoriesServices from '../services/InventoriesServices';

export default {
    props: {
        inventory: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            editInventory: {
                id: 0,
                userId: null,
                inventoryName: "",
                
            },
        };
    },

    computed: {
        initializedInventory() {
            return {
                id: this.inventory.inventoryId || 0,
                userId: this.inventory.userId || null,
                inventoryName: this.inventory.inventoryName || "",
            };
        },
    },

    // this is for the lifecycle hook when the component is creating to grab the userId from the store
    
    methods: {
        submitForm() {
            if (!this.validateAddForm()) {
                return;
            }
            if (this.editInventory.id === 0) {
                InventoriesServices.addInventory(this.editInventory)
                .then(response => {
                    if(response.status === 201) {
                        this.$router.push({name: 'home'});
                    }
                })
            } else {
                InventoriesServices.updateInventory(this.editInventory)
                .then(response => {
                    if(response.status === 200) {
                        this.$router.push({name: 'home'});
                    }
                })
            }

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
        cancelForm() {
            this.$router.push({name: 'home'});
        },

        resetForm() {
            this.editInventory = {
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