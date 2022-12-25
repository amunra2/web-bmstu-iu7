import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

interface Server {
    id: Number
    name: String
    ip: String
    gameName: String
    rating: Number
    status: Number
    hostingId: Number
    platformId: Number
    countryId: Number
    ownerId: Number
}

const client = axios.create({
    baseURL: 'http://localhost:5555/api/v1/servers',
})

export default {
    name: "ServerInterface",

    execute(method: any, resource: any, data?: any, params?: any) {
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

    getAll(page: number | null = null, pageSize: Number | null = null) {
      return this.execute('get', '/', null, {page, pageSize});
    },
}

