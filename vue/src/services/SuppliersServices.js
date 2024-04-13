import axios from "axios";

const http = axios.create({
    baseURL: "http://localhost:53041"
});

export default {
    getSuppliers(){
        return axios.get(`/suppliers`);
    },
    // we need to create a supplier
    addSupplier(supplier){
        return axios.post(`/suppliers`, supplier);
    },
}