import axios from "axios";

// unsure about this part
const http = axios.create({
    baseURL: "http://localhost:53041"
  });

export default {
    // the endpoint and parameters in the function could change
    getInventories(){
        return axios.get(`/Inventories`);
    },

    getInventory(inventoryId) {
        return axios.get(`/Inventories/${inventoryId}`)
    },

    addInventory(inventory){
        return axios.post(`/Inventories`, inventory);
    },

    updateInventory(inventory) {
        return axios.put(`/Inventories/${inventory.inventoryId}`, inventory);
    }
    
}