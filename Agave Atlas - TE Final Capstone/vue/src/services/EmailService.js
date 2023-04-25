import axios from 'axios';

const serve = axios.create({
    baseURL: "https://localhost:44315/"
});

export default {
    sendTestEmail(details){
        return serve.post('Email/testEmail', details)
    },

    inviteNewUser(details){
        return serve.post('Email/invite', details)
    }
}