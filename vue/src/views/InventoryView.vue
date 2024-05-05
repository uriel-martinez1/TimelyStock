<template>
    <div>
        <inventory-detail :id="$route.params.inventoryId" />

        <item-list />

        <!--We need an add item button under the list-->
        <div>
            <h2>Add new Item</h2>
            <button v-on:click="addItem">Add Item</button>
        </div>

        <div v-if="notification" class="notification" :class="notification.type">
            {{ notification.message }}
        </div>
    </div>
</template>

<script>
import InventoryDetail from '../components/InventoryDetail.vue';
import ItemList from '../components/ItemList.vue';

export default {
    components: {
        InventoryDetail,
        ItemList
    },
    methods: {
        addItem() {
            this.$router.push({name: "AddItemView", params: {inventoryId: this.$route.params.inventoryId}});
        }
    },
    computed: {
        notification() {
            return this.$store.state.notification;
        }
    }
};
</script>

<style>
.notification {
  position: fixed;
  top: 20px;
  right: 20px;
  padding: 10px 20px;
  border-radius: 5px;
  color: #fff;
  z-index: 9999;
}

.success {
  background-color: green;
}

.error {
  background-color: red;
}
</style>