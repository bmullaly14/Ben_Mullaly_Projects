<template>
  <div>
    <navigation-bar />
    <div v-if="isLoading" class="loading">
       <div class="loading-text">Getting ready to join the fiesta... 🍹🎉</div>
      <img src="../assets/MargaritaLoader.gif"/>
    </div>
    <div class="twitter-div" v-else>
     <div class="content-wrapper">
  <div id="left-side">
    <div class="sticky-container">
          <div class="share-container" id="share">
            <div class="share-text">
              <h2>Tell us what you think!</h2>
              <p>Share a picture of your favorite margarita:</p>
            </div>
            <a href="https://twitter.com/share?ref_src=twsrc%5Etfw" class="twitter-share-button" data-size="large" data-text="Hey @AgaveAtlas, just visited your site and loved the [insert feature]. Thanks for making my margarita experience even better! 🍹 #AgaveAtlas #FindYourFavoriteMargSpot #MargaritaLove" data-hashtags="MargaritaDayIsEveryday" data-show-count="false">Tweet to @AgaveAtlas</a>
          </div>
          <div class="share-container" id="invite">
            <invite-user-form/>
          </div>
        </div>
        </div>
        <a id="twitter" class="twitter-timeline" data-lang="en" data-theme="dark" href="https://twitter.com/AgaveAtlas?ref_src=twsrc%5Etfw">Tweets by AgaveAtlas</a>
        <script type="application/javascript" async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
      </div>
    </div>
  </div>
</template>

<script>
import InviteUserForm from '../components/InviteUserForm.vue';
import NavigationBar from '../components/NavigationBar.vue';

export default {
  components: { NavigationBar, InviteUserForm },
  name: "SocialsPage",
  data(){
    return{
      isLoading:true
    }
  },
  mounted(){
    setTimeout(() => {
      this.isLoading = false
    }, 3000)
  }
};
</script>

<style scoped>
.loading {
  margin-top: 5rem;
  max-width: 800px;
  margin: 0 auto;
  text-align: center;
}

.loading-text {
  margin: 15rem;
  font-size: 1.5rem;
  margin-bottom: 1rem;
}

.loading img {
  width: 200px;
  height: 200px;
  object-fit: cover;
}

.twitter-div {
  position: relative;
  top: 80px; /* adjust this value based on the height of your navigation-bar */
  margin: 0 auto;
  max-width: 1000px;
  background-color: #f2f2f2;
  border-radius: 8px;
  box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
  padding: 1rem;
}

.twitter-timeline {
  display: flex;
  justify-content: center;
  text-overflow: ellipsis;
}

.share-container {
  position: sticky;
  top: 5rem;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-color: #f2f2f2;
  padding: 1rem;
  border-radius: 8px;
  box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
  margin: 1rem 0;
  z-index: 1;
}

.share-text {
  text-align: center;
  margin-bottom: 1rem;
}

.twitter-share-button {
  font-size: 1rem;
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  background-color: #1da1f2;
  color: white;
  transition: background-color 0.2s ease-in-out;
  text-decoration: none;
  text-align: center;
  justify-content: center;
  margin-bottom: 1rem;
}

.twitter-share-button:hover {
  background-color: #0c8de4;
}

.twitter-timeline {
  height: 400px;
  max-width: 500px;
}

.timeline-TweetList {
  font-size: 14px;
  line-height: 1.4em;
}

.timeline-Tweet-text {
  margin: 0;
}

.twitter-div {
  position: relative;
  top: 80px; /* adjust this value based on the height of your navigation-bar */
  margin: 0 auto;
  max-width: 1000px;
  background-color: #f2f2f2;
  border-radius: 8px;
  box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
  padding: 1rem;
}

/* #share{
  grid-area: share;
} */

/* #left-side{
  position: sticky;
  grid-area: left;
} */

.content-wrapper {
  display: grid;
  grid-template-columns: auto 1fr;
  grid-gap: 1rem;
  align-items: start;
  grid-template-areas: 
  "left tweet";
}

.share-text p {
  width: 100%;
  white-space: pre-wrap;
  word-wrap: break-word;
}

/* .invite-container {
  display: flex;
  position: sticky;
  top: 14rem;
  z-index: 0;
} */

.sticky-container {
  position: sticky;
  top: 5rem;
  z-index: 0;
}

/*this fixed the scroll effect for the left side*/
#left-side{
  position: sticky;
  top: 5rem;
  z-index: 0;
}
/* .share-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-color: #f2f2f2;
  padding: 1rem;
  border-radius: 8px;
  box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
  margin: 1rem 0;
} */



</style>
