import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

export interface User {
    id: Number,
    Login: String,
    Role: String
}

const client = axios.create({
    baseURL: 'http://localhost:5555/api/v1/users',
    validateStatus: function (status) {
        return status < 500;
    }
})

export default {
    async execute(method: any, resource: any, data?: any) {
        return client({
                    method,
                    url: resource,
                    data,
                    headers: { }
                });
    },

    login (login: String, password: String) {
        return this.execute('post', '/login', {login, password});
    },

    register (login: String, password: String) {
        return this.execute('post', 'register', {login, password});
    },

    getAll() {
        return this.execute('get', '/');
    },

    async getFavorites() {
        return await this.execute('get', '/');
    }
}

