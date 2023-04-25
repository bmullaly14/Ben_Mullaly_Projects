<template>
  <div>
      <div id="fullDay">
          <span id="info">
          <p>{{period.name}}</p>
          <p id="details">{{period.detailedForecast}}</p>
          </span>
      </div>
  </div>
</template>

<script>
export default {
    name: "weather-widget",
    data(){
        return{
            hover: false
        }
    },
props: ["period"]
}
</script>

<style scoped>
#info{
    display: flex;
    flex-direction: column;
    
}
#info{
    max-height: 100%;
    width: 100%;
}
p{
    display:inline-block;
    margin: 5px;
}

</style>