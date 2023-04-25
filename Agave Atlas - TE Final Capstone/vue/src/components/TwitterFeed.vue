<template>
  <div>
  </div>
</template>

<script>
export default {
  name: "TwitterFeed",
  mounted() {
    const script = document.createElement("script");
    script.setAttribute("src", "https://platform.twitter.com/widgets.js");
    script.setAttribute("charset", "utf-8");
    document.body.appendChild(script);
  }
};
</script>

<style scoped>

  

</style>