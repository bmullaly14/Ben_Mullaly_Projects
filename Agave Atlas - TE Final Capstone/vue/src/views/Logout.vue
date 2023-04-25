<template>
  <div id="logout">
    <navigation-bar />
    <div class="image-container">
       <img src="../assets/MargaritaMascotSadFace.png" class="mascot">
      <img src="../assets/tearGif.gif" class="tear">
    </div>
    <h1>Logout</h1>
    <div v-if="isAuthenticated">
      <p>You have successfully logged out.</p>
      <router-link to="/login">Ready to rejoin the fiesta? Log back in and let's get this party started again!</router-link>
    </div>
    <div v-else>
      <p>You are not currently logged in.</p>
      <router-link to="/login">Click here to log in.</router-link>
    </div>
  </div>
</template>

<script>
import NavigationBar from '../components/NavigationBar.vue';
export default {
  components: { NavigationBar },
  name: "Logout",
  data() {
    return {
      isAuthenticated: false,
    };
  },
  created() {
    this.isAuthenticated = this.$store.state.token !== "";
    this.$store.commit("LOGOUT");
  },
};
</script>
<style>
#logout {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100vh;
    font-family: Verdana, Geneva, Tahoma, sans-serif;

}
.image-container {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 50px;
}

.image-container img {
  max-width: 300px;
}
.mascot {
  position: relative;
  max-width: 300px;
}

.tear {
  position: absolute;
  top: 46%;
  left: 48%;
  transform: translate(-50%, -50%);
  max-width: 100px;
  z-index: 1;
}
</style>
