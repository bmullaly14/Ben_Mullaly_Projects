<template>
  <div id="yelp_card">
    <!-- <map-component v-bind:restaurant="restaurant" /> -->
    <h2>{{ restaurant.name }}</h2>
    <div class="container">
      <!-- <div>
        <img width="20%" :src=restaurant.imageUrl />
      </div> -->
      <div id="address">
        <h4>Address:</h4>
        <p>{{ restaurant.address }},<br>
          {{ restaurant.city }}, {{ restaurant.state }} {{ restaurant.zipCode }}
        </p>
      </div>
      <div id="phone">
        <h4>Phone:</h4>
        <p>{{ restaurant.displayPhone }}</p>
      </div>
      <div id="info">
        <h4>Info:</h4>
        <p>
          This restaurant has {{ restaurant.rating }} stars based on
          {{ restaurant.reviewCount }} reviews on Yelp! <br>
        <a :href="restaurant.yelpUrl">See more on Yelp. </a></p>
      </div>
    </div>
  </div>
</template>

<script>
//import MapComponent from "../components/MapComponent.vue"

export default {
  name: "yelp-component",
  props: ["restaurant"],
  //components: {MapComponent},
  data() {
    return {};
  },
  created() {},
};
</script>

<style scoped>
#yelp_card {
  border: 5px;
  border-color: #7bc950;
  background: #f3faef;
  width: 80vw;
  font-family: Verdana, Geneva, Tahoma, sans-serif;

}
.container {
  display: flex;
  justify-content: space-around;
  background: #FFFFFF;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  text-align: center;

}
h2{
  margin: 0px;
  text-align: center;
  font-family: Verdana, Geneva, Tahoma, sans-serif;

}


</style>