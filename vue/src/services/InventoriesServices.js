import axios from "axios";

// unsure about this part
const http = axios.create({
    baseURL: "http://localhost:53041"
  });

export default {
    // the endpoint and parameters in the function could change
    getInventories() {
        return axios.get(`/inventories`);
    },

    getInventoryDetail(inventoryId) {
        return axios.get(`/inventories/${inventoryId}`);
    },

    addInventory(inventory) {
        return axios.post(`/inventories`, inventory);
    },

    updateInventory(inventory) {
        return axios.put(`/inventories/${inventory.inventoryId}`, inventory);
    },

    deleteInventory(inventoryId) {
        return axios.delete(`/inventories/${inventoryId}`);
    }, 
    
    getItemsByInventoryId(inventoryId) {
        return axios.get(`/inventories/${inventoryId}/item`);
    },

    getItemByInventoryIdAndItemId(inventoryId, itemId) {
        return axios.get(`/inventories/${inventoryId}/item/${itemId}`);
    },
    
    addItemByInventoryId(item, inventoryId) {
        return axios.post(`/inventories/${inventoryId}/item`, item);
    },

    updateItemByInventoryId(inventoryId, itemId, item) {
        return axios.put(`/inventories/${inventoryId}/item/${itemId}`, item);
    },

    deleteItemByInventoryId(inventoryId, itemId) {
        return axios.delete(`/inventories/${inventoryId}/item/${itemId}`);
    }
}