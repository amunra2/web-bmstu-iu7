import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

export interface Server {
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
    validateStatus: function (status) {
        return status < 500;
    }
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
        })
    },

    getAll(page: number | null = null, pageSize: Number | null = null) {
      return this.execute('get', '/', null, {page, pageSize});
    },

    getById(id: number) {
        return this.execute('get', `/${id}`);
    },

    post (server: Server) {
        return this.execute('post', '/', server, null);
    },

    put (id: number, server: Server) {
        return this.execute('put', `/${id}`, server, null);
    }
}

