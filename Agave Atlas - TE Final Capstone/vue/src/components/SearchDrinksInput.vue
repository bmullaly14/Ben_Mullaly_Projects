<template>
  <div>
    <div>
      <nav-bar/>
    </div>
    <div id="search-stuff">
      <div id="input-for-search">
      <label class="col-form-label-lg" for="drink-search">Search All Drinks</label>
      <input id="drink-search" class="form-control" name="drink-search" type="text" placeholder="Lime" v-model="filterValue">
      </div>
      <input id="search-button" type="button" class="btn success" value="Search" v-on:click="setFilter();"/>
      <h3 v-show="noResults" class="fa-shake">No results!</h3>
      <h4 v-show="noResults">Please try again</h4>
    </div>
    <div class="drink-card-grid">
      
      <searched-drink-card class="list-group-item" v-for="drink in filterDrinks" v-bind:drink="drink" v-bind:key="drink.id" v-on:click="routeToPage($event); "/>
     
    </div>
  </div>
</template>

<script>
import NavBar from './NavigationBar.vue'
import DrinkService from '../services/DrinkService.js'
import SearchedDrinkCard from '../components/SearchedDrinkCard.vue'
export default {
  components: {SearchedDrinkCard, NavBar},
  name: "search-drinks-input",
data(){
    return{
        filterValue: '',
        drinks: [],
        filterDrinks: [],
        componentKey: 0,
        noResults: false,
    }
},
methods: {
  setFilter(){
    this.applyFilter();
    this.componentKey+=1;
  },
  
  applyFilter(){
    if(this.filterValue == null){
      
      this.filterDrinks = this.drinks;
    }
    this.filterDrinks = this.drinks.filter(drink =>
    {
      let filter = this.filterValue.toLowerCase();
      let desc = drink.description.toLowerCase();
      if(desc.includes(filter) || drink.name.includes(filter)){
          return this.makeDrink(drink);
    }})
    if(this.filterDrinks.length === 0){
      this.noResults = true;
    } else{this.noResults = false;}
  },
  makeDrink(drink){
    let newDrink = {
      drinkID: JSON.stringify(drink.drinkID),
      drinkName: JSON.stringify(drink.name),
      description: JSON.stringify(drink.description),
      isFrozen: JSON.stringify(drink.isFrozen),
      restaurantID: JSON.stringify(drink.restaurantID),
      restaurantName: JSON.stringify(drink.restaurantName)
    };
    
    return newDrink;
  }
},
created(){
  console.log(DrinkService.getAllDrinks());
  DrinkService.getAllDrinks().then(response =>
  {
    this.drinks = response.data;
    
  }).catch(error => {
    console.log(error.message);
  })
}
}
</script>

<style scoped>
#cards-div{
  height: 100%;
  width: 100%;
}
#search-stuff{
  display: flex;
  flex-direction:column;
  flex-wrap: wrap;
  margin: 150px auto;
  justify-content: center;
  align-content: center;
  width: 30vw;
  height: 30vh;
  background-color:#e0ebdb;
  border: none;
  border-radius: 15px;
}
#input-for-search{
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;
  align-content: center;
  width: 50%;
}
#search-button{
  margin: 15px auto 0 auto;
  width: 25%;
}
#search-button:hover{
  color: white;
  background-color: #6db743;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.4);
}
#drink-search{
  margin: 0px 15px;
}
label{
  margin: 0;
}
.drink-card-grid{
  width: 80%;
  margin: auto;
}
h3{
  text-align: center;
  color: red;
}
h4{
  text-align: center;
}
.fa-shake{
  --fa-animation-iteration-count: 1;
}
</style>