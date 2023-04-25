<template>
  <div class="card container" v-on:click="seeDrinks()">
    
     <button @click.prevent.stop="deleteRestaurant()" v-if='this.$store.state.token !== "" && this.UserRole=="admin"' class="delete-button"><font-awesome-icon :icon="['fas', 'fa-trash']" /></button>
    <img width="100%" :src=restaurant.imageUrl>
    <!-- <img width="100%" :src="require(`@/assets/${restaurant.name.replace(/\s+/g, '')}${restaurant.zipCode}.jpg`)"> -->
    <div class="centered">
      <h2>{{ restaurant.name }}</h2>

      <p>{{restaurant.address}}</p>
      <p>{{restaurant.city}}, {{restaurant.state}} {{restaurant.zipCode}}</p>
    </div>
  </div>
</template>

<script>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { fas } from "@fortawesome/free-solid-svg-icons";
import { library } from "@fortawesome/fontawesome-svg-core";
library.add(fas);

import zipcodeService from '../services/ZipCodeService.js';
import RestaurantService from '../services/RestaurantService.js';
// import yelpService from "../services/YelpService.js";

export default {
  components: {FontAwesomeIcon},
    name: "restaurant-card",
    props: ["restaurant"],
    data(){
      return{
        UserRole: this.$store.state.user.role,
        locationData: [],
        // yelpInput:{
        //   name: this.restaurant.name,
        //   zipcode: this.restaurant.zipCode
        // },
        // yelpInfo:{
        //   id: ""
        // }
      }
    },
    methods: {
    seeDrinks() {
      this.$router.push(`/restaurant/${this.restaurant.id}`);
    },
    deleteRestaurant() {
      if (confirm("Are you sure you want to delete this restaurant?")) {
        RestaurantService.deleteRestaurant(this.restaurant.id).then((response) => {
          if (response.status === 204) {
            location.reload();
             alert("Successfully deleted.")
            this.$emit("deleteRestaurant");
          }
        });
      }
    },
  },


  computed:{
    cardClass(){
      const name = this.restaurant.name.toLowerCase().replace(/[^a-z0-9]+/g, `_`);
      return `card-${name}_bg`;
    },
    imageUrl() {
      return `../assets/${this.restaurant.imageFilePath}`;
    },
  },
  created() {
      zipcodeService.GetCityByZipcode(this.restaurant.zipCode).then((response) => {
        this.locationData = response.data.places[0];
        this.state = response.data.places.state;
        
      });
      // yelpService.GetBusinessByNameAndZip(this.yelpInput).then((response) => {
      // this.yelpInfo.id = response.data.id;
      // console.log(this.yelpInfo)
      // })
      // .catch(error => {
      //   console.log(error)})
      }
    
};
</script>

<style scoped>
.card {
  display: flex;
  flex-direction: column;
  background-color: #fff;
  border: 5px solid #eaeaea;
  /* border-radius: 10px; */
  margin-bottom: 20px;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  width: 30%;
  height: 300px;
  background-size: cover;
  background-position: top;
  position: relative;
  text-align: center;
  color: white;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.4);
  transition: 0.3s;
  cursor: pointer;
}

.card:hover {
 box-shadow: 0 8px 16px 0 rgba(0,0,0,0.6);
}

h2 {
  text-align: center;
}

.card-header {
  background-color: #7bc950;
  border-bottom: 1px solid #eaeaea;
  padding: 10px;
}

.card-body {
  padding: 10px;
}

.centered {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background: rgb(0, 0, 0); 
  background: rgba(0, 0, 0, 0.7);
  padding: 20px;
}

.delete-button {
    position: absolute;
    top: 10px;
    right: 10px;
    background-color: red;
    color: white;
    border: none;
    border-radius: 5px;
    padding: 5px 10px;
    cursor: pointer;
  }
img{
  height: fit-content;
}
.card img{
  height: 100%;
  width:100%;
  object-fit: cover;
}
</style>