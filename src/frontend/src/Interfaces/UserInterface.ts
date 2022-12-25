import axios from "axios";

axios.defaults.baseURL = 'http://localhost:5555/';
axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

interface User {
    id: Number,
    Login: String,
    Role: String
}

const client = axios.create({
    baseURL: 'http://localhost:5555/api/v1/users',
})

export default {
    async execute(method: any, resource: any, data?: any) {
        return client({
            method,
            url: resource,
            data,
            headers: { }
        }).then(req => {
            return req.data
        })
    },

    login (login: String, password: String) {
        return this.execute('post', '/loign', {login, password}) // что возвращать должен?
    },

    async getAll() {
        return await this.execute('get', '/');
    },

    async getFavorites() {
        return await this.execute('get', '/');
    }
}

