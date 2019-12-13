<template>
  <div>
    <li class="nav-item" v-for="link in links" v-bind:key="link.link">
     <!-- // <a v-bind:href="link.link" class="nav-link">{{link.name}}</a> -->
      <router-link :to="link.link">{{link.name}}</router-link>
    </li>
  </div>
</template>

<script>
export default {
  props: ["links"],
  computed: {
    getLink(obj) {
      return obj.link;
    }
  }
};
</script>
<style  scoped>
.custom {
  background: none;
  color: #ffffff;
}
</style>