<template>
  <div id="app">
    <div class="d-flex" id="wrapper">
      <Sidebar></Sidebar>
      <div id="page-content-wrapper">
        <PageContent v-bind:title="$route.name"></PageContent>
        <router-view></router-view>
        <Footer></Footer>
      </div>
    </div>
  </div>
</template>
<script>
import Sidebar from "./admin/layout/Sidebar";
import PageContent from "./admin/layout/PageContent";
import Footer from "./admin/layout/Footer";
export default {
  name: "app",
  components: {
    Sidebar,
    PageContent,
    Footer
  },
  data() {
    return {
      str : "abc"
    };
  },
  methods: {
    
  }
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
  color: #2c3e50;
}

main {
  text-align: center;
  margin-top: 40px;
}

header {
  margin: 0;
  height: 56px;
  padding: 0 16px 0 24px;
  background-color: #f8f9fa;
  color: #ffffff;
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