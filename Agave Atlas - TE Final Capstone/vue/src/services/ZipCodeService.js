import axios from 'axios';

const http = axios.create({
    baseURL: "https://api.zippopotam.us/us/"
});


export default {
    GetCityByZipcode(zipcode){
        return http.get(zipcode)
    },
    GetZipcodeByCityAndState(city, state){
        return http.get(`${state}/${city}`)
    }
}