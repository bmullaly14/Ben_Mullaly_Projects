<template>
  <div>
      <form id="email-form" v-show="!showPreview">
          <div class="form-group">
              <h2>Invite a friend to the fiesta!</h2>
              <div id="send" class="form-input">
        <label for="SendName" class="form-label"> Your Name: </label>
        <input id="SendName" type = "text" class="form-control"
            name="SendName" v-model="details.sender"
             required>
            </div>
            <div id="rec" class="form-input">
        <label for="RecName" class="form-label"> Recipient Name: </label>
        <input id="RecName" type = "text" class="form-control"
           name="RecName" v-model="details.recepient" required>
            </div>
            <div id="mail" class="form-input">
        <label for="RecEmail" class="form-label"> Recipient Email: </label>
        <input id="RecEmail" type="email" class="form-control"
            name="RecEmail" v-model="details.email" required>
            </div>
            <div id="error" v-if="errorList && errorList.email">{{errorList.email}}</div>
        <button class="submitButton" v-on:click.prevent=" clearErrors(); validateEmail();">Submit</button>
        </div>
      </form>
      <div id="preview" v-show="showPreview">
          <p class="preview-field" id="toName">To: {{details.recepient}}</p>
          <p class="preview-field" id="toEmail">At: {{details.email}}</p>
          <p class="preview-field" id="fromName">From: {{details.sender}}</p>
          <button class="submitButton" v-on:click.prevent="sendEmail()">Confirm and Send</button>
          <button class="submitButton" v-on:click.prevent="showPreview = false;">Cancel</button>
      </div>
  </div>
</template>

<script>

import EmailService from "../services/EmailService.js"

export default {
    data() {
        return{
            details: {
                recepient:'',
                email: '',
                sender: this.$store.state.user.username
            },
            emailSent: false,
            sendError: false,
            showPreview: false,
            errorList: null
        }
    },
methods: {
    sendTestEmail(){
        EmailService.sendTestEmail(this.details).then(
            response => {
                if(response.data){
                    alert("Invitation Successfully Sent!")
                    this.emailSent = true;
                    this.showPreview = false;
                    this.details = {
                        recepient:'',
                        email: '',
                        sender: this.$store.state.user.username};
                } else{
                    this.sendError = true;
                    alert(`${response.status} : ${response.statusText}`);
                }
            }
        )
    },
    sendEmail(){
        EmailService.inviteNewUser(this.details).then(
            response => {
                if(response.data){
                    alert("Invitation Successfully Sent!")
                    this.emailSent = true;
                    this.showPreview = false;
                    this.details = {
                        recepient:'',
                        email: '',
                        sender: this.$store.state.user.username};
                } else {
                    this.sendError = true;
                    alert(response.status + `There was an error inviting ${this.details.recepient}. Please try again later!`);
                }
            }
        )
    },
    validateEmail(){
        let errors = {};
        this.errorList = null;
      if (!this.details.email) {
        errors.email = 'Email is required!';
      } else if (!/^[^@]+@\w+(\.\w+)+\w$/.test(this.details.email)) {
        errors.email = 'Invalid email!';
      }

      if (Object.keys(errors).length > 0) {
        this.errorList = errors;
        return;
      } else {
        this.errorList = null;
        this.showPreview = true;
        this.emailSent = false;
      }
    },
    clearErrors(){
        this.errorList = null;
    }
}
}
</script>

<style scoped>
.form-group{
    display: flex;
     position: sticky;
  top: 80px; /* adjust this value based on the height of your navigation bar */
  z-index: 1; /* ensure the form appears above other elements */
    flex-direction: column;
    justify-items: center;
    align-content: center;
    flex-wrap: wrap;
    width: 70%;
    margin: auto;
}
.form-input{
    margin: .5rem;
    padding: .5rem;
    width: 100%;
    text-align: center;
}
.submitButton{
    margin: 5px;
  padding: 5px 15px;
  background-color: #7bc950;
  color: white;
  border: none;
  border-radius: 20px;
  cursor: pointer;
}
.submitButton:hover{
    background-color: #6db743;
}
.form-label {
    display:flex;
    width: 100%;
    flex-direction: row;
    justify-content: center;
}
#error{
    text-align: center;
    color: black;
    background-color: #7bc950;
    border: none;
    border-radius: 20px;
    padding: 5px 10px;
    width: fit-content;
    margin: auto;
}
#preview {
    display: flex;
    flex-direction: column;
    justify-content: center;
    /* width: 100%; */
    margin: auto;
    justify-items: center;
    flex-wrap: wrap;
    align-content: center;
}
.preview-field{
    /* width: 100%; */
    margin: auto;
    padding: 10px;
}
</style>