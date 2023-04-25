<template>
  <div>
    <h1>Map for {{ restaurant.name }}:</h1>
    <div id="map" ref="mapRef"></div>
  </div>
</template>

<script>
//import mapService from "../services/MapService.js";
import { onMounted, ref } from "vue";

export default {
  name: "map-component",
  props: ["restaurant"],
  setup() {
    const mapRef = ref(null);
    onMounted(() => {
      const tt = window.tt;
      var map = tt.map({
        key: "aWGmJIlu6NtAoTWh855jGMLmVBJUnwyx",
        //key: process.env.local.TOMTOM_API_KEY,
        container: mapRef.value,
        style: "tomtom://vector/1/basic-main",
      });
      map.addControl(new tt.FullscreenControl());
      map.addControl(new tt.NavigationControl());
      addMarker(map);
    });
    return {
      mapRef,
    };
    function addMarker(map) {
      const tt = window.tt;
      var location = [-81.709940000000003, 41.483739999999997];
      var popupOffset = 25;

      var marker = new tt.Marker().setLngLat(location).addTo(map);
      var popup = new tt.Popup({ offset: popupOffset }).setHTML(
        "Your address!"
      );
      marker.setPopup(popup).togglePopup();
    }
  },
  methods: {
    LocationArray() {
      return [-81.709940000000003, 41.483739999999997];
    },
  },
};
</script>

<style scoped>
#map {
  height: 50vh;
  width: 50vw;
}
</style>