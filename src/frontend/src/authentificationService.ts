import UserInterface from "./Interfaces/UserInterface";

interface User {
    id: Number,
    Login: String,
    Role: String
}

export default {
    async login(login: String, password: String) {
        //const result = await UserInterface.login(login, password); // использование метода (добавить!)
    
        console.log("Login", login, password);
        console.log(this.getCurrentUser());
    
        if (login == "user" && password == "user") {
            const user: User = {
                id: 1,
                Login: "user",
                Role: "user"
            }
    
            localStorage.setItem('currentUser', JSON.stringify(user));
        }
        else if (login == "admin" && password == "admin") {
            const user: User = {
                id: 2,
                Login: "admin",
                Role: "admin"
            }
    
            localStorage.setItem('currentUser', JSON.stringify(user));
        }
    },
    
    getCurrentUser() {
        return JSON.parse(String(localStorage.getItem("currentUser")));
    },
    
    logout() {
        const user: User = {
            id: 0,
            Login: "guest",
            Role: "guest"
        }
    
        localStorage.setItem('currentUser', JSON.stringify(user));
        // localStorage.removeItem('currentUser');
    }
}
