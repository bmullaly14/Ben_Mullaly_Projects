import axios from 'axios';

// const apiKey = process.env.local.YELP_API_KEY;

// const http = axios.create({
//     baseURL: "https://api.yelp.com/v3/businesses/search?"
// });


export default {
    GetBusinessByNameAndZip(yelpinput) {
        //let nameandzip = {yelpinput};
        return axios.post('/YelpApi/', yelpinput);
    }
}