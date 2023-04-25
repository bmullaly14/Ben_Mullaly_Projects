<template>
  <div class="list">
    <div class="restaurant-cards">
      <restaurant-card
        v-for="restaurant in restaurants"
        v-bind:key="restaurant.id"
        v-bind:restaurant="restaurant"
        v-bind:class="restaurantCardClass(restaurant.name)"
         @deleteRestaurant="deleteRestaurant"
      />
    </div>
    <div>
      <add-restaurant-form :is-authenticated="isAuthenticated" />
    </div>
  </div>
</template>

<script>
import RestaurantCard from "./RestaurantCard.vue";
import RestaurantService from "../services/RestaurantService.js";
import AddRestaurantForm from './AddRestaurantForm.vue';
//import authService from "../services/AuthService.js";

export default {
  methods: {
    restaurantCardClass(name) {
      const className = name.toLowerCase().replace(/ /g, '-');
      return `card-${className}`;
    },
    deleteRestaurant(id){
      RestaurantService.deleteRestaurant(id)
      .then(() =>{
        this.restaurants = this.restaurants.filter((restaurant) => restaurant.id !== id )
      })
    },
    fetchRestaurants() {
      RestaurantService.getRestaurants()
        .then((response) => {
          this.restaurants = response.data;
        })
        .catch((error) => {
          if (error) {
            console.log(error);
          }
        });
    },
  },
  name: "restaurant-list",
  components: {
    RestaurantCard,
    AddRestaurantForm,
  },
  data() {
    return {
      restaurants: [],
      isAuthenticated: false, // Set to false by default
    };
  },

  created() {
    this.fetchRestaurants();
    this.isAuthenticated = this.$store.state.token !== "";
  },
};
</script>

<style>
.restaurant-cards {
  display: flex;
  flex-direction: row;
  flex-flow: wrap;
  justify-content: space-between;
}

div.main {
  margin: 1rem 0;
  display: flex;
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
  background-color: #7bc950;
  border-bottom: 1px solid #eaeaea;
  padding: 10px;
}

.card-body {
  padding: 10px;
}
.list {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}
</style>
