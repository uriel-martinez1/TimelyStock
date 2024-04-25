import axios from "axios";

const http = axios.create({
    baseURL: "http://localhost:53041"
});

export default {
    getCategories(){
        return axios.get(`/categories`);
    },
    
    addCategory(category){
        return axios.post(`/categories`, category);
    },
}