<template>
  <div class="drink-list-container">
    
    <div class="drink-card-grid">
      <!-- <h1>{{restaurant.name}}</h1> -->
      <!-- <button v-on:click=GenerateYelpInfo>Get Info</button>
      <div v-if="haveYelpData">
          <p> Yelp ID : {{yelpReturn.yelpId}}</p>
          <p> Address : {{yelpReturn.address}}, {{yelpReturn.city}}, {{yelpReturn.state}}</p>
        </div>
      </div> -->
    
      <drink-card
      class = "list-group-item"
      v-for="drink in drinks" 
      v-bind:key="drink.id"
      v-bind:drink="drink"/>
    </div>
  </div>
</template>

<script>

import DrinkCard from "./DrinkCard.vue";
import RestaurantService from "../services/RestaurantService.js"
//import YelpComponent from './YelpComponent.vue';
//import yelpService from "../services/YelpService.js";

export default {
    components: {DrinkCard
    
    },
data() {
    return {
        drinks: [],
        restaurant: {},
        yelpReturn: {},
        haveYelpData: false
    }
},
computed: {},
created(){
    RestaurantService.getDrinksByRestaurant(this.$route.params.id)
    .then((response) => {
        this.drinks = response.data;
        console.log(this.drinks);
    })
    .catch((error) => {
        if(error) {
            console.log(error);
        }
    });
    RestaurantService.getRestaurant(this.$route.params.id)
      .then((response) => {
        this.restaurant = response.data;
        
      })
      .catch((error) => {
        if (error) {
          console.log(error);
        }
      });
  },
  methods: {
    // GenerateYelpInfo(){
    //   let yelpSearch = {
    //       name: this.restaurant.name,
    //       zipcode: this.restaurant.zipCode};
    //   yelpService.GetBusinessByNameAndZip(yelpSearch).then((response) => {
    //   this.yelpReturn = response.data;
    //   this.haveYelpData = true;
    //   console.log(this.yelpReturn)
    //   })
    //   .catch(error => {
    //     console.log(error)});
    //}
  }
}
</script>

<style>
.drink-list-container{
  margin: 10px 20px;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}

.drink-card-grid{
  display: grid;
  grid-template-columns: repeat(4,minmax(200px, 1fr));
  grid-gap: 1rem;
  
  margin: 12px 0 0px 0;
}

.drink-card{
  border: 1px black solid;
  font-family: Verdana, Geneva, Tahoma, sans-serif;

}

h1{
  text-align: center;
    font-family: Verdana, Geneva, Tahoma, sans-serif;

}


div.main {
  margin: 1rem 0;
}
div.main div.well-display {
  display: flex;
  justify-content: space-around;
  margin-bottom: 1rem;
}

div.main div.well-display div.well {
  display: inline-block;
  width: 15%;
  border: 1px black solid;
  border-radius: 6px;
  text-align: center;
  margin: 0.25rem;
  padding: 0.25rem;
  
}

.card-header {
  background-color: rgb(123, 201, 80);
  border: none;
  border-radius: 20px;
  padding: 10px;
}

.card-body {
  padding: 10px;
}

div.card.list-group-item {
  background-color: #F3FAEF;
}

</style>