<template>
  <div class = "card" v-on:click="routeToPage()">
    <div class = "card-header" >
      <h2>{{decodeHtml(drink.name.split(' ').map(word => word.charAt(0).toUpperCase() + word.slice(1)).join(' '))}}</h2> 
      <h4> |-{{drink.restaurantName}}-| </h4>
    <div class = "card-body">
      <p>{{decodeHtml(drink.description)}}</p>
      <p class="frozen-text" v-if="drink.isFrozen">It's Frozen! <font-awesome-icon :icon="['fasr', 'snowflake']" flip style="color: #78ddf2; font-size: large;" /></p>
    </div>
  </div>
  </div>
</template>

<script>
import drinkService from '../services/DrinkService.js'
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

export default {
  components:{
    FontAwesomeIcon
  },
  data(){
    return {
      drinkId : this.drink.drinkID,
      restaurantID : this.drink.restaurantID,
      UserRole: this.$store.state.user.role,
    }
  },
  props: ["drink","drink.drinkID"],
  methods: {
    decodeHtml(html) {
      var txt = document.createElement("textarea");
      txt.innerHTML = html;
      return txt.value;
    },
    deleteDrink(){
      if(this.UserRole === "admin"){
      if(confirm("Are you sure you wish to delete?")){
      drinkService.deleteDrink(this.drinkId).then((response)=> {
        if(response.status === 204){
          location.reload();
          alert("Successfully deleted.")
        }
      
      }).catch((error) => {
        if (error) {
          console.log(error);
        }
      });
      }
      }
      else{
        alert("you are not permitted to delete this!")
      }
    },
    pushToForm(){
      this.$router.push({ name: "update-drink", params: { id: this.$route.params.id, drinkId: this.drinkId } });
    }, 
    routeToPage(){
    console.log(this.restaurantID);
    this.$router.push({name: "restaurant-view", params: {id: this.restaurantID}});
  },
  }
}

</script>

<style scoped>
.card {
  position: relative;
  display: flex;
  flex-direction: column;
  background-color: rgb(255, 255, 255);
  border: #7ac950 2px solid;
  border-radius: 15px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.4);
  margin-bottom: 20px;
}

.card:hover {
 box-shadow: 0 8px 16px 0 rgba(0,0,0,0.6);
}

h2{
  text-align: center;
  margin: 2px;
  padding: 3px;
  font-size: 1.5em;
}

.card-header {
  background-color: #7bc950;
  border: none;
  border-radius: 20px;
  padding: 10px;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  /* height: 80px; */
  display: flex;
  flex-flow: column;
  justify-content: center;
  align-items: center;
}

.card-body {
  padding: 10px;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}

.cardBtn{
  margin: 5px;
  padding: 5px 15px;
  background-color: white;
  color: black;
  border: none;
  border-radius: 20px;
  cursor: pointer;
}

.btns{
  display: flex;
  /* flex-flow: row; */
  justify-content: center;
  align-items: center;
}

.frozen-text {
  text-align: right;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  
}
</style>