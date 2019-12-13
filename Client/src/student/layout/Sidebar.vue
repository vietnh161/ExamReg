<template>
  <nav id="sidebar">
    <ul class="list-unstyled components">
      <li class="nav-item">
        <h3 class="custom">Student page</h3>
      </li>
      <SidebarLink :links="links"></SidebarLink>
    </ul>
  </nav>
</template>
<script>
import SidebarLink from "./SidebarLink";

export default {
  name: "Sidebar",
  components: {
    SidebarLink
  },
  data() {
    return {
      links: [
        {
          name: "Trang chủ",
          link: "/student/home"
        },
        {
          name: "Đăng kí lịch thi",
          link: "/student/dang-ki-lich-thi"
        },
        {
          name: "In danh sách lịch thi",
          link: "/student/in-lich-thi"
        }
       
        
      ]
    };
  },
  methods: {}
};
</script>
<style scoped>
.custom{
  text-align: center;
  padding-top: 13px;
}
</style>