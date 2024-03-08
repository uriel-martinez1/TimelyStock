import axios from "axios";

// unsure about this part
const http = axios.create({
    baseURL: "http://localhost:53041"
  });

export default {
    // the endpoint and parameters in the function could change
    getInventories(){
        return axios.get(`/inventories`);
    },

    getInventory(inventoryId) {
        return axios.get(`/inventories/${inventoryId}`)
    },

    addInventory(inventory){
        return axios.post(`/inventories`, inventory);
    },

    updateInventory(inventory) {
        return axios.put(`/inventories/${inventory.inventoryId}`, inventory);
    },
    
}