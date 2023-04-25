<template>
  <div id="theWholeThing" ref="theWholeThing">
      <div id="theInfoThing">
          <div id="weatherHead">
      <h2>Forecast for {{cityName}}, {{stateAbb}}</h2>
      <div id="changeZipStuff">
          <span id="zipChange" v-show="changingZip">
            <label class="label" for="zipCode">Input Zip Code</label>
            <input type="text" pattern="[0-9]*" id="zipCode" name="zipCode" placeholder="44118" v-model="zipCode"/>
            <div id="buttons">
            <input class="btn success" type="button" v-on:click="updateWeather(zipCode); changingZip = false;" value="Go"/>
            <input class="btn danger" type="button" v-on:click="changingZip = false" value="Cancel"/>
            </div> 
        </span>
        <span id="wantToChangeZip" v-show="!changingZip">
                <input id="zipButton" class="btn success" type="button" v-on:click="changingZip = true" 
                value="Change Location"/>
        </span>
      </div>
      </div>
      <div id = "allWeather">
            <div id="forecastWeather">
                <span class = "dayNames" v-for="period in periods" v-bind:key="period.id"
                v-on:mouseover="filterCurrent(period)">{{period.name}}
                </span>
            </div>
            <span id="highlightedWeather">
                <div id="currentWeather" >
                    <weather-widget v-bind:period="periods[filter]"/>
                </div>
            </span>
      
      </div>
      </div>
  </div>
</template>


<script>
import Weather from '../services/WeatherService.js'
import WeatherWidget from './WeatherWidget.vue'
import ZipCodeService from '../services/ZipCodeService'

export default {
    components: {WeatherWidget},
data(){
    return {
        periods: [],
        filter: 0,
        zipCode: '',
        newLat: '',
        newLong: '',
        cityName: 'Cleveland',
        stateAbb: 'OH',
        changingZip: false
    }
},
methods: {
    filterCurrent(num) {
        this.filter = this.periods.indexOf(num);
        this.weatherBackground();
    },
    setWeatherPeriods(resp){
        this.periods = [];
        resp.data.properties.periods.filter(per => {
                if (!per.name.includes('Night')) {
                    let chunk = {
                        number: JSON.parse(JSON.stringify(per.number)),
                        name: JSON.parse(JSON.stringify(per.name)),
                        detailedForecast: JSON.parse(JSON.stringify(per.detailedForecast))
                    }
                    this.periods.push(chunk);
    }})},
    updateWeather(zip){
        
        ZipCodeService.GetCityByZipcode(zip).then(response =>
        {
            
            this.newLat = response.data.places[0].latitude;
            this.newLong = response.data.places[0].longitude;
            this.cityName = response.data.places[0]["place name"];
            this.stateAbb = response.data.places[0]["state abbreviation"];
            
            Weather.GetWeatherByCoordinate(this.newLat, this.newLong).then(response => {
               
            this.setWeatherPeriods(response);
            this.filter = 0;
            this.weatherBackground();
        }

        ).catch(error => {
            
               alert(`${error.response.status}: Error retrieving weather data for ${this.cityName}`);
            
        })
        }).catch(error => {
        alert(`${error.response.status}: Error finding City with zip code "${this.zipCode}"`);
           
        });
        
    },
    weatherBackground(){
       if(this.$refs.theWholeThing.classList.contains('sunny')){
           this.$refs.theWholeThing.classList.remove('sunny');
       } else if (this.$refs.theWholeThing.classList.contains('cloudy')){
           this.$refs.theWholeThing.classList.remove('cloudy');
       } else if (this.$refs.theWholeThing.classList.contains('rain')){
           this.$refs.theWholeThing.classList.remove('rain');
       } else if(this.$refs.theWholeThing.classList.contains('snow')){
           this.$refs.theWholeThing.classList.remove('snow');
       } else if (this.$refs.theWholeThing.classList.contains('partlyCloudy')){
           this.$refs.theWholeThing.classList.remove('partlyCloudy');
       }
        if(this.periods[this.filter].detailedForecast.includes("snow") || this.periods[this.filter].detailedForecast.includes("Snow")) {
            this.$refs.theWholeThing.classList.add('snow');}
        else if(this.periods[this.filter].detailedForecast.includes("howers") || this.periods[this.filter].detailedForecast.includes("ain")){
            this.$refs.theWholeThing.classList.add('rain');
        } else if(this.periods[this.filter].detailedForecast.includes("loudy")){
            if(this.periods[this.filter].detailedForecast.includes("unny"))
            {this.$refs.theWholeThing.classList.add('partlyCloudy');}
            else {this.$refs.theWholeThing.classList.add('cloudy');}
        
        } else if(this.periods[this.filter].detailedForecast.includes("unny") || this.periods[this.filter].detailedForecast.includes("lear")){
           // console.log("sunny");
            this.$refs.theWholeThing.classList.add('sunny');
        }
    }
},
created() {
    Weather.GetWeatherCuyahoga().then(response =>
        {
            response.data.properties.periods.filter(per => {
                if (!per.name.includes('Night')) {
                    let chunk = {
                        number: JSON.parse(JSON.stringify(per.number)),
                        name: JSON.parse(JSON.stringify(per.name)),
                        detailedForecast: JSON.parse(JSON.stringify(per.detailedForecast))
                    }
                    //console.log(chunk);
                    this.periods.push(chunk);
                    //console.log(this.periods[0].name);
                }
                
            });     
            this.weatherBackground();
        }).catch(error =>
    {
        if(error){
            console.log(error);
        }
    })
}
}
</script>

<style scoped>
#allWeather{
    display: flex;
    flex-direction: column;
    height: 70%;
    align-items: center;
    justify-content: space-between;
}
#forecastWeather{
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    grid-area: next;
    width: 80%;
    justify-content: space-around;
    
};
#currentWeather{
    display: flex;
    flex-direction: column;
    width: 100%;
    max-height: 100%;
    grid-area: today;
    
}
#mainW{
display: grid;
  align-items: center;
  justify-content: center;
  grid-template-areas: 
  "today next next next next next next"
  "today next next next next next next";
  height: 100px;
  text-overflow: ellipsis;
  background-color: #dcf1d0;
  border-style:solid;
  border-color:black;
  border-radius: 15px;
}
#highlightedWeather{
    /* padding: 20px; */
    margin: 10px;
    border:2px solid black;
    border-style: solid hidden none hidden;
    max-width: 100%;
    height: 100%;
    inline-size: 80%;
    position: inherit;
    
}
#name{
    margin: 0;
}
#theInfoThing{
    background: rgb(243,250,239,0.8);
    z-index: -1;
    
}
.widgets{
    width:fit-content;
}
#weatherHead {
    display: grid;
    justify-content: space-between;
    align-items: center;
    grid-template-areas: 
    ". header . zip";
    grid-template-columns:1fr 2fr .3fr .7fr;
    margin: 0 0 10px 0;
    height: 85px;
    
}
h2{
    /* margin: 0 0 20px 0; */
    display: inline-block;
    grid-area: header;
    position:sticky;
    text-align: center;
}
.sunny, .partlyCloudy{
    background-image: url('../assets/weatherPics/blueSky.jpg');
    background-repeat: no-repeat;
    background-size: 100%;
    background-position: center;
    
}
.rain {background-image: url('../assets/weatherPics/rain.jpg');
    background-repeat: no-repeat;
    background-size: 100%;
    background-position: center;
    }
.cloudy{
    background-image: url('../assets/weatherPics/clouds.jpg');
    background-repeat: no-repeat;
    background-size: 100%;
    background-position: center;
    
}
.snow{
    background-image: url('../assets/weatherPics/snow.jpg');
    background-repeat: no-repeat;
    background-size: 100%;
    background-position: center;
}
#changeZipStuff{
    display: inline-flex;
    flex-direction: column;
    grid-area: zip;
    align-self:baseline;
    justify-self: center;
    justify-content: center;
    margin: auto;
}
#zipChange {
    display: flex;
    flex-wrap: wrap;
    flex-direction: column;
    text-align: center;
    align-items:center;
    width:initial;
}
#zipCode{ 
    width:90%;
} 
#buttons>input{
    display:inline;
    margin: 5px;
    padding: 2px 4px;
}
#zipButton{
    width: fit-content;
    margin: 5px;
}
.btn:hover{
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.4);
    color: white;
    background-color: #6db743;
}
.label{
    margin: 2px;
}
</style>