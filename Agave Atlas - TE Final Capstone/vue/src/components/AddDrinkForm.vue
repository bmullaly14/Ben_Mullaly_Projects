<template>
  <div class = "drinkForm">
    <button class= "addDrink btn success" v-if="isLoggedIn" v-show="!showAddDrinkForm" v-on:click="openForm">Not seeing what you're sipping? Add a Drink!</button>    
    <div class="form-popup" id="myForm">
      <form class="form-container"   v-show="showAddDrinkForm">
        <h3>Add a Drink!</h3>
        
        <label for="name">Drink Name:</label>
        <input class="text-box" id="name" type="text" v-model="drink.Name"/>

        <label for="description">Description:</label>
        <input class="text-box" id="description" type="text" v-model="drink.Description"/>

        <div class="frozen">
          <label for='isFrozen'>Frozen:</label>
          <input id="isFrozen" type="checkbox" v-model="drink.IsFrozen"/>
        </div>

        <div class="btns">
          <button class="bton"  type='submit' v-on:click.prevent="submitForm" >Submit</button>
          
          <button  class="bton" v-on:click.prevent="showAddDrinkForm = false;">Cancel</button>
        </div>
    
      </form>
    </div>
  </div>
</template>

<script> 
import drinkService from '../services/DrinkService.js'

export default {
name: "drink-form",
data(){
  return{
    drink: {
      Name:'',
      Description:'',
      IsFrozen:false,
      RestaurantID: Number(this.$route.params.id)
    },
    showAddDrinkForm : false,
    isLoggedIn : false,
  }
},
created() {
    this.isLoggedIn = this.$store.state.token !== "";
},
methods:{
  openForm() {
    const form = document.getElementById("myForm");
    form.style.display = "block";
    this.showAddDrinkForm = true;
  },
  submitForm(){
    // let newDrink = {
    //   restaurantID: Number(this.$route.params.id),
    //   Name: this.name,
    //   Description: this.description,
    //   isFrozen: this.isFrozen
    // }
    //newDrink = JSON.stringify(newDrink);
    drinkService.addNewDrink(this.drink).then((response)=>{
      if(response.status === 200){
        location.reload();
      }
    })
    .catch((error) => {
        if (error) {
          console.log(error);
        }
      });
  
    //location.reload() 
    //this.resetDrink();

  },
  resetDrink(){
    this.drink = {
      name:'',
      description:'',
      isFrozen:false,
      RestaurantID: this.$route.params.id,
    }
  }

}
}

 </script>

<style scoped>
  .drinkForm{
    /* margin-top:100px;
    font-family:sans-serif; */
    font-family: Verdana, Geneva, Tahoma, sans-serif;

  }
/* input{
  margin-left:10px;
}
label{
  margin-left:10px;
}
button{
  margin-left:10px;
} */

.bton, .btn{
  width:150px;
  background-color: #7bc950;
  color: white;
  border: none;
  border-radius: 20px;
  cursor: pointer;
  margin-right:10px;
}
.text-box{
  width:250px;
}
/* .form{
  border-style:solid;
  border-color:#7bc950;
  height: 100%;
  display:flex;
  flex-direction: column;
  justify-content: space-evenly;
  height:250px;
}
h3{
  text-align:center;
}


.addDrink {
  margin: 5px 46.5%;
  padding: 5px 15px;
  background-color: #7bc950;
  color: white;
  border: none;
  border-radius: 20px;
  cursor: pointer;
} */

.addDrink {
  /* background-color: #555; */
  color: white;
  padding: 16px 20px;
  border: none;
  cursor: pointer;
  opacity: 0.8;
  position: sticky;
  bottom: 20px;
  left: 10px;
  width: 280px;
  z-index: 9;
  
}
.addDrink:hover{
  background-color: #6db743;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.4);
}

.form-popup {
  display: none;
  position: fixed;
  bottom: 0;
  left: 15px;
  border: 3px solid #f1f1f1;
  z-index: 9;
}

/* Add styles to the form container */
.form-container {
  max-width: 300px;
  padding: 10px;
  background-color: white;
  
}

.form-container input[type=text], .form-container input[type=password] {
  /* width: 100%; */
  padding: 15px;
  /* margin: 5px 0 22px 0; */
  margin-bottom: 15px;
  border: none;
  background: #f1f1f1;
}

.form-container input[type=text]:focus, .form-container input[type=password]:focus {
  background-color: #ddd;
  outline: none;
}

.form-container .btn {
  background-color: #04AA6D;
  color: white;
  padding: 16px 20px;
  border: none;
  cursor: pointer;
  width: 100%;
  margin-bottom:10px;
  opacity: 0.8;
}

.form-container .cancel {
  background-color: red;
}

.form-container .btn:hover, .open-button:hover {
  opacity: 1;
}
.btns{
  display: flex;
  justify-content: center;
}
.frozen{
  display: flex;
  margin-bottom: 10px;
}
.btn {
  margin: auto;
}
</style>