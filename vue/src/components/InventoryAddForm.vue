<template>
    <form v-on:submit.prevent="submitForm">
        <div class="field">
            <label for="name">Inventory Name:</label>
            <input type="text" id="name" name="name" :value="inventory.inventoryName" @change="changeName($event)" />
        </div>
        <button>Save</button>
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
                inventoryId: this.inventory.inventoryId,
                userId: this.inventory.userId,
                inventoryName: this.inventory.inventoryName,
                
            },
        };
    },
    computed: {
        notification() {
            return this.$store.state.notification;
        },
    },
    methods: {
        submitForm() {
            if (!this.validateAddForm()) {
                return;
            }
            // create new inventory
            console.log(this.editInventory);
            if (this.editInventory.inventoryId === undefined || this.editInventory.inventoryId === 0) {
                console.log(this.editInventory)
                InventoriesServices.addInventory(this.editInventory)
                .then(response => {
                    if(response.status === 201) {
                        // we will add a notification for when an inventory is created
                        this.$store.commit(
                            'SET_NOTIFICATION',
                            {
                                message: 'A new inventory was added.',
                                type: 'success'
                            }
                        );
                        this.$router.push({name: 'home'});
                    }
                })
                .catch ((error) => {
                    this.$store.commit(
                        'SET_NOTIFICATION',
                        {
                            message: 'Failed to create new inventory. Please try again.',
                            type: 'error'
                        }
                    )
                })
            } else {
                // edit existing inventory
                console.log(this.editInventory);
                InventoriesServices.updateInventory(this.editInventory)
                .then(response => {
                    if(response.status === 200) {
                        this.$store.commit(
                            'SET_NOTIFICATION',
                            {
                                message: 'Inventory was updated.',
                                type: 'success'
                            }
                        );
                        this.$router.push({name: 'home'});
                    }
                })
                .catch ((error) => {
                    this.$store.commit(
                        'SET_NOTIFICATION',
                        {
                            message: `Failed to update inventory called ${this.editInventory.inventoryName}. Please try again.`,
                            type: 'error'
                        }
                    )
                })
            }
        },

        changeName(event) {
            this.editInventory.inventoryName = event.target.value;
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
            this.editInventory.inventoryId = this.inventory.inventoryId;
            this.editInventory.userId = this.inventory.userId;
            return true;
        },
        cancelForm() {
            this.$router.back();
        },
    }
}
</script>

<style>
</style>