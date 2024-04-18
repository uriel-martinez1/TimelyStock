import axios from 'axios';

const http = axios.create({
    baseURL: "http://localhost:53041"
});

export default {
    // we moved the add item to the inventories service
}