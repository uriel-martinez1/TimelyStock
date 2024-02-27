import axios from "axios";

export default {
    // the endpoint and parameters in the function could change
    getInventories(userId){
        return axios.get(`/inventories/${userId}`);
    },
}