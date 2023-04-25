import axios from 'axios';

const http = axios.create({
    baseURL: "https://api.weather.gov"
});

export default {
GetWeatherCuyahoga(){
    return http.get('/zones/county/OHZ011/forecast')
},

async GetWeatherByCoordinate(lat, long){
    let office = await http.get(`/points/${lat},${long}`)
    return http.get(`/gridpoints/${office.data.properties.gridId}/${office.data.properties.gridX},${office.data.properties.gridY}/forecast`);
    }
}
