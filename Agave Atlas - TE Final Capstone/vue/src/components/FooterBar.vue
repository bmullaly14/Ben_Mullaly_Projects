<template>
  <footer>
    <div class="container">
      <div class="row">
        <div class="col-md-4">
          <h4>Contact Us</h4>
          <p>Email: agaveatlas@gmail.com</p>
          </div>
          <div class = "col-md-4">
          <p>Address: 7100 Euclid Ave #140, Cleveland, OH 44103</p>
          </div>
        
        <div class="row bottom-row">
        <div class="col-md-12 github-icons">
          <h4>Follow Us</h4>
          <ul class="social-icons">
            <li>
              <a href="https://twitter.com/AgaveAtlas">
                <font-awesome-icon :icon="['fab', 'twitter']" />
              Twitter</a>
            </li>
                    <li>
                        <a href="https://github.com/bmullaly14">
                            <font-awesome-icon class="fa-lg" :icon="['fab', 'github-alt']" style="color: purple" />
                        Ben Mullaly</a>
                    </li>
                    <li>
                        <a href="https://github.com/jfrancioso">
                            <font-awesome-icon class="fa-lg" :icon="['fab', 'github-alt']" style="color: green" />
                        Joseph Francioso</a>
                    </li>
                    <li>
                        <a href="https://github.com/tahnijae">
                            <font-awesome-icon class="fa-lg" :icon="['fab', 'github-alt']" style="color: black" />
                        Tahni Hresko</a>
                    </li>
                    <li>
                        <a href="https://github.com/user4">
                            <font-awesome-icon class="fa-lg" :icon="['fab', 'github-alt']" style="color: red" />
                        Wilson Warholic</a>
                    </li>
                    <li>
                        <a href="https://github.com/DaleJMitchell">
                            <font-awesome-icon class="fa-lg" :icon="['fab', 'github-alt']" style="color: blue" />
                        Dale Mitchell</a>
                    </li>
                </ul>
        </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <hr>
          <p class="text-center">&copy; 2023 AgaveAtlas. All rights reserved.</p>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { fab } from "@fortawesome/free-brands-svg-icons";

library.add(fab);

export default {
    components: {
        FontAwesomeIcon
    },
    
};
</script>

<style>
footer {
  background-color: #333;
  color: white;
  padding: 1em;
  
  bottom: 0;
  left: 0;
  right: 0;
}

.twitter-button {
  font-size: 2.5rem;
}

#footer {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
}

#footer_name img {
  max-height: 3em;
}

#footer_buttons {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  margin-top: 1em;
}

#footer_buttons button {
  background-color: transparent;
  border: none;
  color: white;
  cursor: pointer;
  font-size: 1rem;
  margin-left: 1em;
}

#footer_buttons button:hover {
  color: #ccc;
}


  /* github styles ... */
  
    .bottom-row {
    margin-top: 1em;
  }

  .github-icons {
    display: flex;
    justify-content: space-between;
    flex-wrap: wrap;
  }

  .github-icons ul {
    display: flex;
    list-style-type: none;
    padding: 0;
    margin: 0;
  }

  .github-icons li {
    margin-right: 2em; /* Add a right margin to create space between items */
  }

  .github-icons li:last-child {
    margin-right: 0; /* Remove right margin from the last item */
  }

  .github-icons li a {
    display: flex;
    align-items: center;
    text-decoration: none;
    color: black;
    
  }

  .github-icons li a:hover {
    text-decoration: underline;
  }

  .github-icons li a font-awesome-icon {
    margin-right: 10px; /* increase the margin-right value */
  }

  h4{
    margin: 0 8px 8px 0;
  }

  .row{
    justify-content: center;
    align-content: center;
    width: 100%;
  }
  .row:last-child{
    justify-items: space-between;
    align-items: center;
  }

</style>
