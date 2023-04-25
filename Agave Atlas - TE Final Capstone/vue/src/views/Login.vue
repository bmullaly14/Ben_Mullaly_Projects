<template>
  <div id="login">
    <navigation-bar />
    <div class="corner top-left"><img src="../assets/marg_2.png" alt="" /></div>
    <div class="corner top-right">
      <img src="../assets/marg_2.png" alt="" />
    </div>
    <div class="corner bottom-left">
      <img src="../assets/marg_2.png" alt="" />
    </div>
    <div class="corner bottom-right">
      <img src="../assets/marg_2.png" alt="" />
    </div>

    <div class="title-container">
      <h1 class="title">Welcome to AGAVE ATLAS!</h1>
    </div>
    <form @submit.prevent="login">
      <h1>Please Sign In</h1>
      <div role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>
      <div role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="form-input-group">
        <div class="column-1">
          <label for="username">Username</label>
          <input type="text" id="username" v-model="user.username" required autofocus />
        </div>
        <div class="column-2">
          <label for="password">Password</label>
          <input type="password" id="password" v-model="user.password" required />
        </div>
      </div>
        
      <button type="submit">Sign in</button>
      <p>
        <router-link :to="{ name: 'register' }"
          >Need an account? Sign up.</router-link
        >
      </p>
    </form>
  </div>
</template>

<script>
import NavigationBar from "../components/NavigationBar.vue";
import authService from "../services/AuthService";

export default {
  name: "login",
  components: { NavigationBar },
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>

<style scoped>
.form-input-group {
  margin-bottom: 1rem;
}
label {
  margin-right: 0.5rem;
}
#login {
  margin-top: 50px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background: repeating-radial-gradient(
    circle at center,
    transparent,
    transparent 50%,
    #dad617 50%,
    #7bc950 70%,
    transparent 70%
  );
    font-family: Verdana, Geneva, Tahoma, sans-serif;

}
.title-container {
  display: flex;
  align-items: center;
  margin-bottom: 2rem;
}
.title {
  font-size: 2rem;
  font-weight: bold;
  font-family: "Helvetica Neue", sans-serif;
  text-transform: uppercase;
  letter-spacing: 0.1rem;
}
.corner {
  position: absolute;
}
.top-left {
    margin-top: 100px;

  top: 0;
  left: 0;

}
.top-right {
    margin-top: 100px;

  top: 0;
  right: 0;
  position: absolute;
}
.bottom-left {
    
    bottom: 0;
  left: 0;
}
.bottom-right {
  bottom: 0;
  right: 0;
  position: absolute;
}
.column-1 {
display: flex; 
margin-bottom: 10px;
}
.column-2 {
display: flex; 
}
input{
  margin: 0px;
}

label{
  margin: 0px;
  width: 100px;
}

button {
  width: 100%;
  margin: 5px;
  padding: 5px 15px;
  background-color: #7bc950;
  color: white;
  border: none;
  border-radius: 20px;
  cursor: pointer;
}

button:hover {
  background-color: #6db743;
}
</style>