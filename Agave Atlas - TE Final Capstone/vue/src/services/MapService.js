// import axios from 'axios';

// const apiKey = process.env.local.TOMTOM_API_KEY;

// const http = axios.create({
//     baseURL: "https://api.yelp.com/v3/businesses/search?"
// });


// export default {
//     GetMap(tt, mapRef){
//         var map = tt.map({
//             key: "aWGmJIlu6NtAoTWh855jGMLmVBJUnwyx",
//             //key: process.env.local.TOMTOM_API_KEY,
//             container: mapRef.value,
//             style: "tomtom://vector/1/basic-main",
//           });
//           map.addControl(new tt.FullscreenControl());
//           map.addControl(new tt.NavigationControl());
//           return map;
//     },
//     GetMapByLatandLong(latitude, longitude) {
//         //return http.get(latitude, longitude);
//     },
//     GetMapByAddress(address, tomtom) {
//         // Initialize the TomTom Maps SDK with your API key

//         // Geocode the address using the TomTom Search API
//         // tomtom.services.fuzzySearch({
//         //     key: apiKey,
//         //     query: address
//         // }).go().then(function (response) {
//         //     // Get the first result from the Search API response
//         //     return response.results[0];

//             // // Get the latitude and longitude coordinates from the Search API result
//             // var lat = result.position.lat;
//             // var lon = result.position.lon;

//             // // Center the map on the latitude and longitude coordinates
//             // map.setCenter([lon, lat]);
//         //});
//     }
// }