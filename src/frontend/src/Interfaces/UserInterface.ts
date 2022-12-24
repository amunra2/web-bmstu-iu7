import axios from "axios";
import {BehaviorSubject} from "rxjs";

axios.defaults.baseURL = 'http://localhost:5555/';

// const currentUserSubject = JSON.parse(String(localStorage.getItem("currentUser")));

interface User {
    id: Number,
    Login: String,
    Role: String
}

export default {
    async login(login: String, password: String) {
        // await axios.post('/user/login/', {login, password});
        if (login == "user" && password == "user") {
            
        }
    }
}