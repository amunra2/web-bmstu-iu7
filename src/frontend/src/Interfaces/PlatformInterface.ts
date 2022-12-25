import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

export interface Platform {
    id: Number
    name: String
    popularity: Number
    cost: Number
}

const client = axios.create({
    baseURL: 'http://localhost:5555/api/v1/platforms',
})

export default {
    name: "PlatformInterface",

    async execute(method: any, resource: any, data?: any, params?: any) {
        return client({
            method,
            url: resource,
            data,
            headers: { },
            params: params
        }).then(req => {
            return req.data
        })
    },

    getAll() {
      return this.execute('get', '/');
    },

    async getById(id: Number) {
        return await this.execute('get', `/${id}`)
    },
}

