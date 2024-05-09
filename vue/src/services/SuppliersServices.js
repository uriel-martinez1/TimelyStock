import axios from "axios";

const http = axios.create({
    baseURL: "http://localhost:53041"
});

export default {
    getSuppliers(){
        return axios.get(`/suppliers`);
    },
    
    addSupplier(supplier){
        return axios.post(`/suppliers`, supplier);
    },

    getSupplierById(supplierId){
        return axios.get(`/suppliers/${supplierId}`);
    },
}