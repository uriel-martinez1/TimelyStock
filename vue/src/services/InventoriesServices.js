import axios from "axios";

// unsure about this part
const http = axios.create({
    baseURL: "http://localhost:53041"
  });

export default {
    // the endpoint and parameters in the function could change
    getInventories(userId){
        return axios.get(`/inventories/${userId}`);
    },
}