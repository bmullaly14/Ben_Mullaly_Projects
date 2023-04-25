<template>
  <div >
      <form  class="update-drink-form">
       
        <h2 class="title">Update this Drink!</h2>
        <label for="name">Drink Name:</label>
        <input id="name" type="text" v-model="newDrink.name" />

        <label for="description">Description:</label>
        <input id="description" type="text" v-model="newDrink.description"/>

        <label for='isFrozen'>Frozen:</label>
        <input id="isFrozen" type="checkbox" v-model="newDrink.isFrozen"/>
        
        <button class = "btn" type='submit' v-on:click.prevent="submitForm">Submit</button>
        <button v-on:click="cancelForm" class="btn" >Cancel</button>
        
      </form>
  </div>
</template>

<script>
import drinkService from '../services/DrinkService.js'
 
export default {
name: "update-drink-form",
props:["drink"],
data(){
    return{
        newDrink : {
            name: '',
            description:'',
            isFrozen:false,
            restaurantID: Number(this.$route.params.id)  
        },
    drinkId : Number(this.$route.params.drinkId),
}
},
methods:{
    submitForm(){
    drinkService.updateDrink(this.drinkId, this.newDrink).then((response)=>{
      if(response.status == 200){
        this.$router.push(`/restaurant/${this.$route.params.id}`)
      }
    })
    .catch((error) => {
        if (error) {
          console.log(error);
        }
      });
    },
    cancelForm(){
        this.$router.push(`/restaurant/${this.$route.params.id}`);
    }
    }

}

</script>

<style>
.update-drink-form{
    margin-top:100px;
    display:flex;
    flex-direction:column;
    width:25%;
    margin-left:37%;
    font-family: sans-serif;
    justify-content: space-evenly;
    height: 300px;
}
.title{
    text-align: center;
    font-family:sans-serif
}
#isFrozen{
    margin-right:90%;
}
.btn{
    margin-left:200px;
    width:100px;
    background-color: #7bc950;
  color: white;
  border: none;
  border-radius: 20px;
  cursor: pointer;
}

</style>