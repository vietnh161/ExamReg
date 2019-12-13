<template>
  <div class="wrapper">
    <Sidebar></Sidebar>
    <div id="content">
      <Navbar v-bind:title="$route.name" :username="user.fullname" @logout="logout"></Navbar>
      <div class="container-fluid">
        <div class="row">
          <main role="main" class="ml-sm-auto col-lg-12 pt-3 px-4">
              <router-view></router-view>
          </main>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import Sidebar from "../admin/layout/Sidebar";
import Footer from "../admin/layout/Footer";
import Navbar from "../admin/layout/Navbar";
import { mapState } from "vuex";

export default {
  components: {
    Sidebar,
    Footer,
    Navbar
  },
  computed:{
    ...mapState(['user'])
  },
  created() {
    this.$store.dispatch("getUserInfo")
  },
  methods: {
    logout: function() {
      this.$store.dispatch("logout").then(() => {
        this.$router.push("/login");
      });
    }
  }
};
</script>
<style scoped>
@import url("./css/styleAdm.css");
</style>