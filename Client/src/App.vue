<template>
  <div id="app">
    <!-- <component :is="layout">
      <router-view :layout.sync="layout"/>
    </component> -->
    <router-view></router-view>
  </div>
</template>
<script>
const default_layout = "default";

export default {
  name: "app",
  components: {},
  data() {
    return {};
  },
  methods: {
  },
  computed: {
    layout() {
      return (this.$route.meta.layout || default_layout) + "-layout";
    }
  },
  // created: function() {
  //   this.$http.interceptors.response.use(undefined, function(err) {
  //     return new Promise(function(resolve, reject) {
  //       if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
  //         this.$store.dispatch(logout);
  //       }
  //       throw err;
  //     });
  //   });
  // }
};
</script>

<style>
body {
  margin: 0;
}

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

main {
  
}

header {
  margin: 0;
  height: 56px;
  padding: 0 16px 0 24px;
}

header span {
  display: block;
  position: relative;
  font-size: 20px;
  line-height: 1;
  letter-spacing: 0.02em;
  font-weight: 400;
  box-sizing: border-box;
  padding-top: 16px;
}
</style>