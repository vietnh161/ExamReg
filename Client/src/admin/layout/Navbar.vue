<template>
  <nav class="navbar navbar-expand-lg navbar-dark navbar-fixed-top">
    <div class="container-fluid">
      <b-button @click="change" style="background-color: Transparent;">
        <font-awesome-icon icon="bars" />
      </b-button>
      <h3>{{title}}</h3>

      <b-navbar-toggle target="navbarSupportedContent"></b-navbar-toggle>

      <b-collapse id="navbarSupportedContent" is-nav>
        <b-navbar-nav class="ml-auto">
          <b-form-select v-model="selected" :options="options" class="mt-1" @input="setkithi"></b-form-select>

          <b-nav-item-dropdown :text="username" right>
            <b-dropdown-item href="#">Profile</b-dropdown-item>
            <b-dropdown-item href="#" @click.stop.prevent="logout">Logout</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </div>
  </nav>
</template>
<script>
import axios from "axios";
import { mapState } from 'vuex'
export default {
  data() {
    return {
      toggle: false,
      options: [],
      selected: null,
    };
  },
  props: {
    title: String,
    username: String
  },

  created() {
    axios
      .get("http://localhost:63834/api/kithi/getall")
      .then(response => {
        let items = response.data;
        let data = new Array();
        let selected = null;
        items.forEach(element => {
          let obj = {
            value: element.kiThiId,
            text: element.name
          };
          data.push(obj);
        });
        this.options = data;
        this.selected = localStorage.kiThiId

      })
      .catch(error => {
        console.log(error.response.data);
      });
  },
  methods: {
    change() {
      this.toggle = !this.toggle;
      if (this.toggle)
        document.getElementById("sidebar").classList.add("active");
      else {
        document.getElementById("sidebar").classList.remove("active");
      }
    },
    setkithi(value){
      localStorage.setItem('kiThiId',value )
    },
     logout() {
      this.$emit("logout", "");
    }
  }
};
</script>
<style scoped>
.nav-custom {
  background-color: #243a51;
}
h3 {
  color: #fff;
  margin-left: 5px;
  margin-bottom: 0px;
}
</style>