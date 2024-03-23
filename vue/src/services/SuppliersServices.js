import axios from "axios";

const http = axios.create({
    baseURL: "http://localhost:53041"
});

export default {
    getSuppliers(){
        return axios.get(`/suppliers`);
    },
}