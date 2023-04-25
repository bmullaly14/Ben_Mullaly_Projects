<template>
  <div class="main" ref="main">
    <navigation-bar />
          <weather-output class="weather"/>
      
    <div class="restaurant-list">
      <br>
      <h2>Locations</h2>
      <br>
      <restaurant-list />
      
    </div>
     <twitter-feed />
     <footer-bar id="footer" />
  </div>
</template>
<script>
import RestaurantList from "../components/RestaurantList.vue";
import WeatherOutput from '../components/WeatherOutput.vue';
import NavigationBar from '../components/NavigationBar.vue'
import authService from "../services/AuthService.js";
import TwitterFeed from "../components/TwitterFeed.vue";
import FooterBar from '../components/FooterBar.vue';



export default {
  name: "Home",
  components: {
    RestaurantList,
    WeatherOutput,
    NavigationBar,
    TwitterFeed,
    FooterBar,

  },
  data() {
    return {
      isAuthenticated: false, // Set to false by default
      restaurants: [],
    };
  },
  created() {
    this.isAuthenticated = this.$store.state.token !== "";
  },
  methods: {
    logout() {
      authService.logout();
      this.$store.commit("LOGOUT");
      this.isAuthenticated = false;
    }
  }
};
</script>
<style scoped>
.restaurant-list {
  text-align: center;
  margin: auto;
  width: 100%;
  grid-area: rest;
}
#banner{
  grid-area: banner;
}
.weather{
  grid-area: weather;
  padding: 20px;
  margin: 5rem 0 0rem 0;
  border: black 2px;
  border-style: solid;
  width: 100%;
  max-width: 100%;
  overflow: hidden;
  text-overflow: ellipsis;
  height: fit-content(5em);
  
}

.main{
  display: grid;
  grid-template-areas: 
  "banner banner banner banner banner"
  ". weather weather weather ."
  ". rest rest rest ."
  "footer footer footer footer footer"; /* Add footer to grid */
  grid-template-columns: .5fr 1fr 4fr 1fr .5fr;
  background-color: #def7d0;
  
  background-size: cover;
  background-position:center;
  background-repeat:no-repeat;
  background-attachment: fixed;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  

}
footer {
  background-color: #F3FAEF;
  color: rgb(24, 23, 23);
  padding: 1em;
  margin: 50px 0 0 0;
  opacity: 1; 
  
  grid-area: footer;
}
#main{
  margin: 16px 0 0 0;
  height: 100vh;
  width: 100vw;
}

h2{
  color: Black;
}

</style>
