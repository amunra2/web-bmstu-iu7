import axios, { AxiosError } from "axios";

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
        return this.execute('post', '/login', {login, password}) // что возвращать должен?
    },

    register (login: String, password: String) {
        return this.execute('post', '/', {login, password}) // что возвращать должен?
    },

    getAll() {
        return this.execute('get', '/');
    },
}

