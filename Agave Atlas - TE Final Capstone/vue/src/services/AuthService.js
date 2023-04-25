import axios from 'axios';

export default {

    login(user) {
        return axios.post('/login', user)
    },
    logout() {
        return axios.post('/logout');
    },
    register(user) {
        return axios.post('/register', user)
    }

}