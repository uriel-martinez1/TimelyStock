import axios from 'axios';

const http = axios.create({
    baseURL: "http://localhost:53041"
});

export default {
    // I am not sure about this?
    addItem(item, inventoryId){
        return axios.post(`/items/${inventoryId}`, item);
    },
}