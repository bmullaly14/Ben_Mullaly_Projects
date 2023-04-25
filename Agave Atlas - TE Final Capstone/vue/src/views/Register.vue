<template>
  <div id="register">
    <navigation-bar />
    <div class="corner top-left">
      <img src="../assets/lime-slice2.png" alt="" />
    </div>
    <div class="corner top-right">
      <img src="../assets/lime-slice2.png" alt="" />
    </div>
    <div class="corner bottom-right">
      <img src="../assets/lime-slice2.png" alt="" />
    </div>
    <div class="corner bottom-left">
      <img src="../assets/lime-slice2.png" alt="" />
    </div>

    <div class="text-center">
      <h1 class="title">Welcome New User!</h1>
    </div>
    <form @submit.prevent="register">
      <h1>Please Create A New Account</h1>
      <div role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
            <div class="form-input-group">

      <div class="column-1">
        <label for="username">Username</label>
        <input
          type="text"
          id="username"
          v-model="user.username"
          required
          autofocus
        />
      </div>
      <div class="column-2">
        <label for="password">Password</label>
        <input type="password" id="password" v-model="user.password" required />
      </div>
      <div class="column-3">
        <label for="confirmPassword">Confirm Password</label>
        <input
          type="password"
          id="confirmPassword"
          v-model="user.confirmPassword"
          required
        />
      </div>
            </div>
      <div>
      <button type="submit">Create Account</button>
      </div>
      <p>
        <router-link :to="{ name: 'login' }"
          >Already have an account? Log in.</router-link
        >
      </p>
    </form>
  </div>
</template>

<script>
import NavigationBar from '../components/NavigationBar.vue';
import authService from "../services/AuthService";

export default {
  name: "register",
  components: {NavigationBar},
  data() {
      return {
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/login",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
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
#register {
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
    #7bc950 50%,
    #7bc950 70%,
    transparent 70%
  );
    font-family: Verdana, Geneva, Tahoma, sans-serif;

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
}
.bottom-left {
  bottom: 0;
  left: 0;
}
.bottom-right {
  bottom: 0;
  right: 0;
}
.column-1 {
display: flex; 
margin-bottom: 10px;
}
.column-2 {
display: flex; 
margin-bottom: 10px;

}
.column-3 {
display: flex; 
}
input{
  margin: 0px;
  width: 100%;
}

label{
  margin: 0px;
  width: 250px;
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
