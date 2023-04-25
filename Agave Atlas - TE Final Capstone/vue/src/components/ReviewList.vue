<template>
  <div id="review-div">
      <div class="not-input-review" v-show="!reviewForm">
        <div id="no-reviews" v-if="reviews.length === 0">
          <h3>Nobody has reviewed {{restaurant.name}} yet!</h3>
          <h4 v-if="$store.state.token === ''">Log in to leave a review!</h4>
          <h4 v-if="$store.state.token != ''"> Click here to be the first!</h4>
        </div>
      <input class="button" type="button" v-on:click="reviewForm = true;" value="Add a Review!" v-if="$store.state.token != ''"/>
      <div id="review-container" :key="componentKey">
            <review-card class="review-card" 
            v-for="(review, id) in reviews" 
            v-bind:key="`${componentKey}-${id}`"
            v-bind:review="review"
            />
      </div>
      </div>
      
      <div > 
      <add-review-form   v-show="reviewForm" @clicked="reviewForm=false; forceUpdate();"/>
      </div>
      
    
  </div>
</template>

<script>
import RestaurantService from '../services/RestaurantService.js'
import ReviewCard from './ReviewCard.vue'
import AddReviewForm from './AddReviewForm.vue'

export default {
    name: "review-list",
    props: ["restaurant"],
components: {
    ReviewCard,
    AddReviewForm
},
data(){
    return{
        reviews:[],
        reviewForm: false,
        newReview: {
            Review_ID: 0,
            Reviewable_ID: this.$route.params.id,
            Rating: 0,
            ReviewerUsername: this.$store.state.user.username,
            Reviewer_ID: 0,
            ReviewText: ''
        },
        componentKey: 0,
        
    }
}
,
created(){
    RestaurantService.getReviewsByRestaurant(this.$route.params.id).then(response => {
        if(response.data != null){
            this.reviews = response.data
            
        }
        
    }).catch(error =>
    {
        if(error){
            console.log(error.status);
        }
    })
},
methods:{
    forceUpdate(){
        RestaurantService.getReviewsByRestaurant(this.$route.params.id).then(response => {
        if(response.data != null){
            this.reviews = response.data
            
        }
        
    }).catch(error =>
    {
        if(error){
            console.log(error.status);
        }
    })
        this.componentKey += 1;
    }
},
computed: {
    checkComplete: () => {
        if(this.newReview.Rating != 0 && this.newReview.ReviewText.length != 0){
            return false;
        } else {
            return true;
        }
    },
}
}
</script>

<style>
#no-reviews{
    text-align: center;
}
.button{
    margin: auto;
  padding: 5px 15px;
  background-color: #7bc950;
  color: white;
  border: none;
  border-radius: 20px;
  cursor: pointer;
  justify-self: center;
}
.button:hover{
    background-color: #6db743;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.4);
}
.not-input-review{
    display:flex;
    flex-direction: column;
    flex-wrap: wrap;
    justify-items: center;
    align-content: center;
}
#review-container{
    width:90%;
}
</style>