<template>
  <div class="addRestaurantForm">
    <button
      class="addRestaurant"
      v-show="isAuthenticated && showMainButton"
      v-on:click="MainButtonClick"
    >
      Looking For Somewhere Else?
    </button>
    <form v-on:submit.prevent="submitForm" v-show="showAddRestaurantForm">
      <div class="form-row">
        <div class="form-group col-md-12" id="name-input">
          <!-- <label for="name">Name:</label> -->
          <input placeholder="Restaurant Name" id="name" type="text" class="input" v-model="restaurantInput.name" />
        </div>
      </div>
      <div class="form-row" id="second-form-row">
        <div class="form-group col-md-4">
          <!-- <label for="city">City:</label> -->
          <input placeholder="City" id="city" type="text" class="input" v-model="restaurantInput.city" />
        </div>
        <div class="form-group col-md-4">
          <!-- <label for="state">State:</label> -->
          <input placeholder="State" id="state" type="text" class="input" v-model="restaurantInput.state" />
        </div>
        <div class="form-group col-md-4">
          <!-- <label for="zipCode">Zipcode:</label> -->
          <input placeholder="Zip Code" id="zipCode" type="text" class="input" v-model="restaurantInput.zipCode"/>
        </div>
      </div>
      <div class="form-row" id="final-row">
        <div class="form-group col-md-6">
      <button @click.prevent="GenerateYelpInfo" class="btn" id="search-button">Search</button>
        </div>
        <div class="form-group col-md-6">
      <button v-on:click="CancelClick" class="btn" id="cancel-button">
        Cancel
      </button>
      </div>
      </div>
    </form>
    <div v-show="haveYelpData">
      <button id="add_restaurant_btn" v-on:click="SubmitForm">Add Restaurant to Agave Atlas!</button>
      <button id="edit_restaurant_btn" v-on:click="showAddRestaurantForm = true">Edit Search</button>
      <button id="cancel_restaurant_btn" v-on:click="CancelClick">Cancel</button>
      <yelp-component v-bind:restaurant="yelpReturn" />
    </div>
  </div>
</template>
<script>
import RestaurantService from "../services/RestaurantService";
import YelpComponent from "./YelpComponent.vue";
import yelpService from "../services/YelpService.js";

//FIX BUTTONS (MAKE METHODS TO SET BOOLS), MAKE ADD METHOD

export default {
  name: "add-new-restaurant",
  props: {
    isAuthenticated: {
      type: Boolean,
      required: true,
    },
  },
  components: { YelpComponent },
  data() {
    return {
      restaurantInput: {
        name: "",
        city: "",
        state: "",
        zipCode: "",
       // RestaurantID: Number(this.$route.params.id),
      },
      showAddRestaurantForm: false,
      showMainButton: true,
      isLoggedIn: false,
      haveYelpData: false,
      yelpReturn: {},
    };
  },
  created() {
    this.isLoggedIn = this.$store.state.token !== "";
  },
  methods: {
    SubmitForm() {
      RestaurantService.addRestaurant(this.yelpReturn).then((response) => {
        if (response.status === 200) {
          this.$emit("restaurantAdded");
          this.showAddRestaurantForm = false;
          this.restaurantInput = {
            name: "",
            city: "",
            state: "",
            zipCode: "",
          };
          location.reload();
          //this.yelpReturn = {};
        }
        else{
          alert("Error creating restaurant!");
        }
      });
    },
    GenerateYelpInfo() {
      if(this.restaurantInput.name == "" || (this.restaurantInput.city == "" && this.restaurantInput.state == "" && this.restaurantInput.zipCode == "")){
        alert("Please enter name and city or zipcode");
      }
      let yelpSearch = {
        name: this.restaurantInput.name,
        zipcode: this.restaurantInput.city + " " + this.restaurantInput.state + " " + this.restaurantInput.zipCode,
      };
      yelpService
        .GetBusinessByNameAndZip(yelpSearch)
        .then((response) => {
          if (response.status === 404){
            alert("Sorry, no results found.")
          }
          this.yelpReturn = response.data;
          this.haveYelpData = true;
          this.showAddRestaurantForm = false;
          this.ShowMainButton = false;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    MainButtonClick(){
      this.showMainButton = false;
      this.showAddRestaurantForm = true;
    },
    CancelClick(){
      this.showMainButton = true;
      this.showAddRestaurantForm = false;
      this.haveYelpData = false;
      this.restaurantInput = {
            name: "",
            city: "",
            state: "",
            zipCode: "",
          };
    }
  },
};
</script>
<style scoped>
.addRestaurant {
  margin: 5px 30%;
  padding: 5px 15px;
  background-color: #7bc950;
  color: white;
  border: none;
  border-radius: 20px;
  cursor: pointer;
}
.addRestaurant:hover{
  color: white;
  background-color: #6db743;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.4);
}
.btn{
  margin: auto;
  margin-left: auto;
  width: 60%;
}
.btn:hover{
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.4);
    color: white;
    background-color: #6db743;
}
#name-input{
  width: 60%;
}
#name{
  width: 60%;
}
form{
  background-color: #F3FAEF;
  padding: 10px;
  border: none;
  border-radius: 15px;
}
#second-form-row{
  width: 60%;
  margin: auto;
}
#final-row{
  width: 60%;
  justify-content: center;
  align-items: center;
  margin: auto;
}
</style>