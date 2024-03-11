<template>
    <form v-on:submit.prevent="submitForm" class="inventoryAddForm">
        <label for="inventoryName">Inventory Name:</label>
        <input id="inventoryName" type="text" class="form-control" v-model="editInventory.inventoryName" />
        <button>Save</button>
        <button v-on:click="cancelForm">Cancel</button>
    </form>
</template>

<script>
import InventoriesServices from '../services/InventoriesServices';

export default {
    props: {
        inventoryObj: {
            type: Object,
            default() {return {}}
        }
    },

    data() {
        return {
            editInventory: {
                id: this.inventoryObj.inventoryId,
                userId: this.inventoryObj.userId,
                inventoryName: this.inventoryObj.inventoryName,
                
            },
        };
    },

    // this is for the lifecycle hook when the component is creating to grab the userId from the store
    
    methods: {
        submitForm() {
            if (!this.validateAddForm()) {
                return;
            }
            // create new inventory
            if (this.editInventory.id === 0) {
                console.log(this.editInventory)
                InventoriesServices.addInventory(this.editInventory)
                .then(response => {
                    if(response.status === 201) {
                        this.$router.push({name: 'home'});
                    }
                })
            } else {
                // edit existing inventory
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
            if(this.editInventory.inventoryName.length === 0) {
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